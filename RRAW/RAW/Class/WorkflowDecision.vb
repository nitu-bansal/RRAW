Option Strict Off
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Reflection
Imports System.CodeDom.Compiler
Imports System.Text.RegularExpressions

Imports System.Linq

Friend Class DECILineItems

    Private errText As String = ""
    Private slOperators As New SortedList()

    Public Sub New()
        MyBase.New()
        slOperators.Add("AND", "&&")
        slOperators.Add("OR", "||")
    End Sub

    Public ReadOnly Property ErrorText() As String
        Get
            Return errText
        End Get
    End Property
    
    Public Function ProcessRulesAndGetDecisionNos(ByVal intRateRequestID As Integer, ByVal ClientID As String, ByVal TransportMode As String) As String
        Dim TrueDecisionNos As String = "0", FalseDecisionNos As String = ""
        Dim ValidationResult As String = ""
        Dim dtWorkflowRules, dtWorkflowRulesCount As DataTable
        Dim drRules() As DataRow
        Dim tl As New WorkflowRules()
        Dim FuncString As String = "", FuncName As String = "", CreateCondition As String = ""
        Dim ValidationSummary As Boolean = False
        Dim dtInputs As DataTable
        Try
            dtInputs = DB.Workflow.GetQuatesByRateRequest(intRateRequestID)
            dtWorkflowRulesCount = DB.Workflow.GetDECIValidationExpression(ClientID, TransportMode, 1)
            dtWorkflowRules = DB.Workflow.GetDECIValidationExpression(ClientID, TransportMode, 0)

            If dtInputs Is Nothing Or dtInputs.Rows.Count = 0 Then
                Return "0"
            End If

            If dtWorkflowRules Is Nothing OrElse dtWorkflowRules.Rows.Count = 0 Then
                errText = "Approvar is yet not decieded, please contact system Administrator."
            ElseIf (dtWorkflowRules.Select("DECNO=0")).Length > 0 Then
                errText = "Decision Code must not be null for this Approval flow, please contact system Administrator."
            Else
                For Each dRowDECNO As DataRow In dtWorkflowRulesCount.Rows
                    drRules = dtWorkflowRules.Select("DECNO=" + dRowDECNO("DECNO").ToString)
                    If (drRules.Count = 1) Then
                        ValidationResult = tl.MapRules(drRules(0), dtInputs, TrueDecisionNos).ToString()
                        If ValidationResult.Length = 4 AndAlso Convert.ToBoolean(ValidationResult) Then
                            TrueDecisionNos += dRowDECNO("DECNO").ToString
                        ElseIf ValidationResult.Length = 5 AndAlso Not Convert.ToBoolean(ValidationResult) Then
                            '    FalseDecisionNos += dRowDECNO("DECNO")
                        Else
                            TrueDecisionNos = "Err"
                        End If
                    ElseIf (drRules.Count > 1) Then
                        FuncString = ""
                        Dim PrevLevel As Integer = 0
                        FuncName = "Eval" & Guid.NewGuid().ToString("N")
                        'ValidationResult = "public bool " + FuncName + "()" + "{ if (";
                        For i As Integer = 0 To drRules.Count - 1
                            ValidationResult = tl.MapRules(drRules(i), dtInputs, TrueDecisionNos).ToString()
                            If ValidationResult.Length = 4 OrElse ValidationResult.Length = 5 Then
                                'TrueDecisionNos += dtWorkflowRules.Rows[0]["ServiceCode"].ToString();
                                ValidationSummary = Convert.ToBoolean(ValidationResult)
                            Else
                                TrueDecisionNos = "Err"
                            End If
                            If TrueDecisionNos <> "Err" Then
                                If drRules(i)("RELAT01").ToString().Trim() <> "" Then
                                    CreateCondition = "( " & ValidationSummary.ToString.ToLower + " " + slOperators(drRules(i)("RELAT01").ToString().Trim())
                                ElseIf PrevLevel <> 0 AndAlso PrevLevel = Convert.ToInt16(drRules(i)("EveluationLevel")) Then
                                    CreateCondition += " " & ValidationSummary.ToString.ToLower & " " + slOperators(drRules(i)("RELAT01").ToString().Trim())
                                Else
                                    CreateCondition += ") " + slOperators(drRules(i)("RELAT01").ToString().Trim()) & " (" & ValidationSummary.ToString.ToLower
                                End If
                                'FuncString = "public bool " + FuncName + "()" + "{ if (" + MapSyntex(FirstValue, SecondValue, dRowValidation["Formula"].ToString()) + "\"){ return true;} else { return false;}}";
                                'Result = DECIValidation.Evaluate(FuncString, FuncName);
                                PrevLevel = Convert.ToInt16(drRules(i)("EveluationLevel"))
                            End If
                        Next
                        CreateCondition += ")"
                        FuncString = "public static bool " & FuncName & "()" & "{ if (" & CreateCondition & "){ return true;} else { return false;}}"
                        ValidationResult = WorkflowRules.Evaluate(FuncString, FuncName).ToString()
                        If ValidationResult.Length = 4 AndAlso Convert.ToBoolean(ValidationResult) Then
                            TrueDecisionNos += dRowDECNO("DECNO").ToString() & ","
                        ElseIf ValidationResult.Length = 5 AndAlso Not Convert.ToBoolean(ValidationResult) Then
                            ' FalseDecisionNos += dRowDECNO("DECNO").ToString() & ","
                        Else
                            TrueDecisionNos = "Err"
                        End If
                        'If dtWorkflowRules.Rows.Count = 1 Then

                        'Else

                        'End If
                    End If
                Next

            End If
            If (TrueDecisionNos <> "0" AndAlso TrueDecisionNos.Substring(0, 1) = "0") Then
                TrueDecisionNos = TrueDecisionNos.Substring(1).Trim(",")
            End If
        Catch Ex1 As Exception
            Throw (New Exception("DECILineItems.GetDecisionNos", Ex1))
        Finally
            'TrueDecisionNos = "0"
        End Try
        Return TrueDecisionNos
    End Function
End Class


Class WorkflowRules
    Public Function MapRules(ByVal dRowValidation As DataRow, ByVal dtInputs As DataTable, Optional ByVal TrueDec As String = "0") As Object
        Dim FuncString As String = "", FuncName As String = ""
        Dim Result As Object = Nothing
        Dim FirstValue As String = "", SecondValue As String = ""
        Dim dRows() As DataRow
        Try
            If Convert.ToString(dRowValidation("FIELDNAM02")).Trim() <> "" AndAlso Convert.ToString(dRowValidation("FIELDCHRFROM")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDCHRTO")).Trim() = "" Then
                FuncName = "Eval" & Guid.NewGuid().ToString("N")
                dRows = dtInputs.Select("FIELD='" + dRowValidation("FIELDNAM01").ToString() + "'")

                Try
                    FirstValue = IIf(dRows IsNot Nothing AndAlso dRows.Count = 1, dRows(0)("VALUE"), "Err")
                Catch ex As Exception
                    FirstValue = "Err"
                End Try



                dRows = dtInputs.Select("FIELD='" + dRowValidation("FIELDNAM02").ToString() + "'")
                Try
                    SecondValue = IIf(dRows IsNot Nothing AndAlso dRows.Count = 1, dRows(0)("VALUE"), "Err")
                Catch ex As Exception
                    SecondValue = "Err1"
                End Try



                FuncString = "public static bool " & FuncName & "()" & "{ if (" & MapSyntex(FirstValue, SecondValue, dRowValidation("Formula").ToString()) & "){ return true;} else { return false;}}"
                Result = WorkflowRules.Evaluate(FuncString, FuncName)
            ElseIf Convert.ToString(dRowValidation("FIELDNAM02")).Trim() <> "" AndAlso Convert.ToString(dRowValidation("FIELDCHRFROM")).Trim() <> "" AndAlso Convert.ToString(dRowValidation("FIELDCHRTO")).Trim() <> "" Then
                FuncName = "Eval" & Guid.NewGuid().ToString("N")
                dRows = dtInputs.Select("FIELD='" + dRowValidation("FIELDNAM01").ToString() + "'")
                If (dRows IsNot Nothing AndAlso dRows.Count = 1) Then
                    FirstValue = dRows(0)("VALUE")
                    FirstValue = FirstValue.Substring(Convert.ToInt16(dRowValidation("FIELDCHRFROM")) - 1, Convert.ToInt16(dRowValidation("FIELDCHRTO"))).ToUpper()
                End If
                dRows = dtInputs.Select("FIELD='" + dRowValidation("FIELDNAM02").ToString() + "'")
                Try
                    SecondValue = IIf(dRows IsNot Nothing AndAlso dRows.Count = 1, dRows(0)("VALUE"), "Err")
                Catch ex As Exception
                    SecondValue = "Err"
                End Try


                'SecondValue = Convert.ToString(dRowInputs(dRowValidation("FIELDNAM02").ToString())).ToUpper()
                FuncString = "public static bool " & FuncName & "()" & "{ if (" & MapSyntex(FirstValue, SecondValue, dRowValidation("Formula").ToString()) & """){ return true;} else { return false;}}"
                Result = WorkflowRules.Evaluate(FuncString, FuncName)
                FirstValue = ""
                SecondValue = ""
            ElseIf Convert.ToString(dRowValidation("FIELDNAM02")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDCHRFROM")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDCHRTO")).Trim() = "" AndAlso Convert.ToString(dRowValidation("COMPVAL01")).Trim() <> "" Then
                dRows = dtInputs.Select("FIELD='" + dRowValidation("FIELDNAM01").ToString() + "'")

                Try
                    FirstValue = IIf(dRows IsNot Nothing AndAlso dRows.Count = 1, dRows(0)("VALUE"), "Err")
                Catch ex As Exception
                    FirstValue = "Err"
                End Try

                'FirstValue = Convert.ToString(dRowInputs(dRowValidation("FIELDNAM01").ToString()))
                SecondValue = Convert.ToString(dRowValidation("COMPVAL01"))
                FuncName = "Eval" & Guid.NewGuid().ToString("N")
                FuncString = "public static bool " & FuncName & "()" & "{ if (" & MapSyntex(FirstValue, SecondValue, dRowValidation("Formula").ToString()) & "){ return true;} else { return false;}}"
                Result = WorkflowRules.Evaluate(FuncString, FuncName)
                FirstValue = ""
                SecondValue = ""
            ElseIf Convert.ToString(dRowValidation("FIELDNAM02")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDCHRFROM")).Trim() <> "" AndAlso Convert.ToString(dRowValidation("FIELDCHRTO")).Trim() <> "" AndAlso Convert.ToString(dRowValidation("COMPVAL01")).Trim() <> "" Then
                FuncName = "Eval" & Guid.NewGuid().ToString("N")
                dRows = dtInputs.Select("FIELD='" + dRowValidation("FIELDNAM01").ToString() + "'")
                If (dRows IsNot Nothing AndAlso dRows.Count = 1) Then
                    FirstValue = dRows(0)("VALUE")
                    FirstValue = FirstValue.Substring(Convert.ToInt16(dRowValidation("FIELDCHRFROM")) - 1, Convert.ToInt16(dRowValidation("FIELDCHRTO"))).ToUpper()
                End If
                SecondValue = Convert.ToString(dRowValidation("COMPVAL01"))
                'FirstValue = Convert.ToString(dRowInputs(dRowValidation("FIELDNAM01").ToString())).Substring(Convert.ToInt16(dRowValidation("FIELDCHRFROM")) - 1, Convert.ToInt16(dRowValidation("FIELDCHRTO"))).ToUpper()
                'SecondValue = Convert.ToString(dRowInputs(dRowValidation("COMPVAL01").ToString())).ToUpper()
                FuncString = "public static bool " & FuncName & "()" & "{ if (" & MapSyntex(FirstValue, SecondValue, dRowValidation("Formula").ToString()) & """){ return true;} else { return false;}}"
                Result = WorkflowRules.Evaluate(FuncString, FuncName)
                FirstValue = ""
                SecondValue = ""
            ElseIf Convert.ToString(dRowValidation("FIELDNAM01")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDCHRFROM")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDCHRTO")).Trim() = "" AndAlso Convert.ToString(dRowValidation("OPERAND")).Trim() = "" AndAlso Convert.ToString(dRowValidation("FIELDNAM02")).Trim() = "" AndAlso Convert.ToString(dRowValidation("COMPVAL01")).Trim() = "" AndAlso Convert.ToString(dRowValidation("RELAT01")).Trim() = "" AndAlso TrueDec = "0" Then
                Result = True
            Else
                Result = False
            End If
        Catch Ex1 As Exception
            Throw (New Exception("BAL.TariffLogics.EvalValidation", Ex1))
        End Try
        Return Result
    End Function

    Public Function MapSyntex(ByVal FirstValue As String, ByVal SecondValue As String, ByVal Formula As String) As String
        Dim Result As String = ""
        Try
            Result = Formula.Replace("[FirstString]", FirstValue).Replace("[SecondString]", SecondValue)
        Catch Ex1 As Exception
            Throw (New Exception("BAL.TariffLogics.MapSyntex", Ex1))
        End Try
        Return Result
    End Function

    Public Shared Function Evaluate(ByVal Expression As String, ByVal FuncName As String) As Object


        If Expression.Length > 0 Then
            'Replace each parameter in the calculation expression with the correct values
            'string MatchStr = "{(\\d+)}";
            'var oMatches = Regex.Matches(Expression, MatchStr);
            'if (oMatches.Count > 0) {
            '    var DistinctCount = (from m in oMatches select m).Distinct.Count;
            '    if (DistinctCount == Args.Length) {
            '        for (int i = 0; i <= Args.Length - 1; i++) {
            '            Expression = Expression.Replace("{" + i + "}", Args[i].ToString());
            '        }
            '    } else {
            '        throw new ArgumentException("Invalid number of parameters passed");
            '    }
            '}

            'string FuncName = "Eval" + Guid.NewGuid().ToString("N");
            Dim FuncString As String = ((((("using System;" + Environment.NewLine & "namespace EvaluatorLibrary") + Environment.NewLine & "{  class Evaluators") + Environment.NewLine & "{  ") + Environment.NewLine & Expression) + Environment.NewLine & "  }") + Environment.NewLine & "}"

            'Tell the compiler what language was used
            Dim CodeProvider As CodeDomProvider = CodeDomProvider.CreateProvider("C#")

            'Set up our compiler options...
            Dim CompilerOptions As New CompilerParameters()
            CompilerOptions.ReferencedAssemblies.Add("System.dll")
            CompilerOptions.GenerateInMemory = True
            CompilerOptions.TreatWarningsAsErrors = False

            'Compile the code that is to be evaluated
            Dim Results As CompilerResults = CodeProvider.CompileAssemblyFromSource(CompilerOptions, FuncString)

            'Check there were no errors...
            If Results.Errors.Count > 0 Then
            Else
                'Run the code and return the value...
                Dim dynamicType As Type = Results.CompiledAssembly.[GetType]("EvaluatorLibrary.Evaluators")
                Dim methodInfo As MethodInfo = dynamicType.GetMethod(FuncName)
                Return methodInfo.Invoke(Nothing, Nothing)

            End If
        Else

            Return 0
        End If

        Return 0

    End Function
End Class


