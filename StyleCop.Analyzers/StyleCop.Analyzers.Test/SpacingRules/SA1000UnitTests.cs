﻿namespace StyleCop.Analyzers.Test.SpacingRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;
    using StyleCop.Analyzers.SpacingRules;
    using TestHelper;
    using Xunit;

    /// <summary>
    /// This class contains unit tests for <see cref="SA1000KeywordsMustBeSpacedCorrectly"/> and
    /// <see cref="SA1000CodeFixProvider"/>.
    /// </summary>
    public class SA1000UnitTests : CodeFixVerifier
    {
        [Fact]
        public async Task TestEmptySource()
        {
            var testCode = string.Empty;
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [Fact]
        public async Task TestCatchallStatement()
        {
            string statement = @"try
{
}
catch
{
}
";

            await this.TestKeywordStatement(statement, EmptyDiagnosticResults, statement);
        }

        [Fact]
        public async Task TestCatchStatement()
        {
            string statementWithoutSpace = @"try
{
}
catch(Exception ex)
{
}
";

            string statementWithSpace = @"try
{
}
catch (Exception ex)
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("catch", string.Empty, "followed").WithLocation(15, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestFixedStatement()
        {
            string statementWithoutSpace = @"byte[] y = new byte[10];
fixed(byte* b = &y[0])
{
}
";

            string statementWithSpace = @"byte[] y = new byte[10];
fixed (byte* b = &y[0])
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("fixed", string.Empty, "followed").WithLocation(13, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestForStatement()
        {
            string statementWithoutSpace = @"for(int x = 0; x < 10; x++)
{
}
";

            string statementWithSpace = @"for (int x = 0; x < 10; x++)
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("for", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestForeachStatement()
        {
            string statementWithoutSpace = @"foreach(int x in new int[0])
{
}
";

            string statementWithSpace = @"foreach (int x in new int[0])
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("foreach", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestFromStatement()
        {
            string statementWithoutSpace = @"var result = from@x in new int[3] select x;";

            string statementWithSpace = @"var result = from @x in new int[3] select x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("from", string.Empty, "followed").WithLocation(12, 26);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestGroupStatement()
        {
            string statementWithoutSpace = @"var result = from x in new[] { new { A = 3 } }
group@x by x.A into z
select z;";

            string statementWithSpace = @"var result = from x in new[] { new { A = 3 } }
group @x by x.A into z
select z;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("group", string.Empty, "followed").WithLocation(13, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestIfStatement()
        {
            string statementWithoutSpace = @"if(true)
{
}
";

            string statementWithSpace = @"if (true)
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("if", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestInStatement()
        {
            string statementWithoutSpace = @"var y = new int[3]; var result = from x in@y select x;";

            string statementWithSpace = @"var y = new int[3]; var result = from x in @y select x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("in", string.Empty, "followed").WithLocation(12, 53);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestIntoStatement()
        {
            string statementWithoutSpace = @"var result = from x in new[] { new { A = 3 } }
group x by x.A into@z
select z;";

            string statementWithSpace = @"var result = from x in new[] { new { A = 3 } }
group x by x.A into @z
select z;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("into", string.Empty, "followed").WithLocation(13, 16);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestJoinStatement()
        {
            string statementWithoutSpace = @"var result = from x in new[] { new { A = 3 } }
join@a in new[] { new { B = 3 } } on x.A equals a.B
select x;";

            string statementWithSpace = @"var result = from x in new[] { new { A = 3 } }
join @a in new[] { new { B = 3 } } on x.A equals a.B
select x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("join", string.Empty, "followed").WithLocation(13, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestLetStatement()
        {
            string statementWithoutSpace = @"var result = from x in new int[3]
let@z = 3
select x;";

            string statementWithSpace = @"var result = from x in new int[3]
let @z = 3
select x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("let", string.Empty, "followed").WithLocation(13, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestLockStatement()
        {
            string statementWithoutSpace = @"lock(new object())
{
}
";

            string statementWithSpace = @"lock (new object())
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("lock", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestOrderbyStatement()
        {
            string statementWithoutSpace = @"var result = from x in new[] { new { A = 3 } }
orderby(x.A)
select x;";

            string statementWithSpace = @"var result = from x in new[] { new { A = 3 } }
orderby (x.A)
select x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("orderby", string.Empty, "followed").WithLocation(13, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestReturnVoidStatement()
        {
            string statementWithoutSpace = @"return;";

            string statementWithSpace = @"return ;";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("return", " not", "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestReturnIntStatement()
        {
            string statementWithoutSpace = @"return(3);";

            string statementWithSpace = @"return (3);";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace, returnType: "int");

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("return", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace, returnType: "int");
        }

        [Fact]
        public async Task TestSelectStatement()
        {
            string statementWithoutSpace = @"var result = from x in new int[3] select@x;";

            string statementWithSpace = @"var result = from x in new int[3] select @x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("select", string.Empty, "followed").WithLocation(12, 47);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestStackallocStatement()
        {
            string statementWithoutSpace = @"int* x = stackalloc@Int32[3];";

            string statementWithSpace = @"int* x = stackalloc @Int32[3];";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("stackalloc", string.Empty, "followed").WithLocation(12, 22);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestSwitchStatement()
        {
            string statementWithoutSpace = @"switch(3)
{
default:
    break;
}
";

            string statementWithSpace = @"switch (3)
{
default:
    break;
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("switch", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestThrowStatement()
        {
            string statementWithoutSpace = @"throw(new Exception());";

            string statementWithSpace = @"throw (new Exception());";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("throw", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestRethrowStatement()
        {
            string statementWithoutSpace = @"try
{
}
catch (Exception ex)
{
    throw;
}
";

            string statementWithSpace = @"try
{
}
catch (Exception ex)
{
    throw ;
}
";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("throw", " not", "followed").WithLocation(17, 5);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestUsingStatement()
        {
            string statementWithoutSpace = @"using(default(IDisposable))
{
}
";

            string statementWithSpace = @"using (default(IDisposable))
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("using", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestWhereStatement()
        {
            string statementWithoutSpace = @"var result = from x in new[] { new { A = true } }
where(x.A)
select x;";

            string statementWithSpace = @"var result = from x in new[] { new { A = true } }
where (x.A)
select x;";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("where", string.Empty, "followed").WithLocation(13, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestWhileStatement()
        {
            string statementWithoutSpace = @"while(false)
{
}
";

            string statementWithSpace = @"while (false)
{
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("while", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestYieldReturnStatement()
        {
            // There is no way to have a 'yield' keyword which is not followed by a space. All we need to do is verify
            // that no diagnostic is reported for its use with a space.

            string statementWithSpace = @"yield return 3;";
            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace, returnType: "IEnumerable<int>");
        }

        [Fact]
        public async Task TestYieldBreakStatement()
        {
            // There is no way to have a 'yield' keyword which is not followed by a space. All we need to do is verify
            // that no diagnostic is reported for its use with a space.

            string statementWithSpace = @"yield break;";
            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace, returnType: "IEnumerable<int>");
        }

        [Fact]
        public async Task TestCheckedStatement()
        {
            string statementWithoutSpace = @"int x = checked(3);";

            string statementWithSpace = @"int x = checked (3);";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("checked", " not", "followed").WithLocation(12, 21);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);

            statementWithoutSpace = @"checked{ };";

            statementWithSpace = @"checked { };";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            expected = this.CSharpDiagnostic().WithArguments("checked", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestDefaultCaseStatement()
        {
            string statementWithoutSpace = @"switch (3)
{
default:
    break;
}
";

            string statementWithSpace = @"switch (3)
{
default :
    break;
}
";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("default", " not", "followed").WithLocation(14, 1);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestDefaultValueStatement()
        {
            string statementWithoutSpace = @"int x = default(int);";

            string statementWithSpace = @"int x = default (int);";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("default", " not", "followed").WithLocation(12, 21);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestNameofStatement()
        {
            string statementWithoutSpace = @"string x = nameof(x);";

            string statementWithSpace = @"string x = nameof (x);";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("nameof", " not", "followed").WithLocation(12, 24);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestSizeofStatement()
        {
            string statementWithoutSpace = @"int x = sizeof(int);";

            string statementWithSpace = @"int x = sizeof (int);";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("sizeof", " not", "followed").WithLocation(12, 21);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestTypeofStatement()
        {
            string statementWithoutSpace = @"Type x = typeof(int);";

            string statementWithSpace = @"Type x = typeof (int);";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("typeof", " not", "followed").WithLocation(12, 22);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestUncheckedStatement()
        {
            string statementWithoutSpace = @"int x = unchecked(3);";

            string statementWithSpace = @"int x = unchecked (3);";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("unchecked", " not", "followed").WithLocation(12, 21);

            await this.TestKeywordStatement(statementWithSpace, expected, statementWithoutSpace);

            statementWithoutSpace = @"unchecked{ };";

            statementWithSpace = @"unchecked { };";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            expected = this.CSharpDiagnostic().WithArguments("unchecked", string.Empty, "followed").WithLocation(12, 13);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestNewObjectStatement()
        {
            string statementWithoutSpace = @"int x = new@Int32();";

            string statementWithSpace = @"int x = new @Int32();";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", string.Empty, "followed").WithLocation(12, 21);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestNewArrayStatement()
        {
            string statementWithoutSpace = @"int[] x = new@Int32[3];";

            string statementWithSpace = @"int[] x = new @Int32[3];";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", string.Empty, "followed").WithLocation(12, 23);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestNewImplicitArrayStatement()
        {
            string statementWithoutSpace = @"int[] x = new[] { 3 };";

            string statementWithSpace = @"int[] x = new [] { 3 };";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            // this case is handled by SA1026, so it shouldn't be reported here
            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);
        }

        [Fact]
        public async Task TestNewConstructorContraintStatement_Type()
        {
            string statementWithSpace = @"public class Foo<T> where T : new ()
{
}";

            string statementWithoutSpace = @"public class Foo<T> where T : new()
{
}";

            await this.TestKeywordDeclaration(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", " not", "followed").WithLocation(9, 39);

            await this.TestKeywordDeclaration(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestNewConstructorContraintStatement_TypeWithMultipleConstraints()
        {
            string statementWithSpace = @"public class Foo<T> where T : IDisposable, new ()
{
}";

            string statementWithoutSpace = @"public class Foo<T> where T : IDisposable, new()
{
}";

            await this.TestKeywordDeclaration(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", " not", "followed").WithLocation(9, 52);

            await this.TestKeywordDeclaration(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestNewConstructorContraintStatement_TypeWithClassConstraints()
        {
            string statementWithSpace = @"public class Foo<T> where T : class, new ()
{
}";

            string statementWithoutSpace = @"public class Foo<T> where T : class, new()
{
}";

            await this.TestKeywordDeclaration(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", " not", "followed").WithLocation(9, 46);

            await this.TestKeywordDeclaration(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestNewConstructorContraintStatement_Method()
        {
            string statementWithSpace = @"public void Foo<T>() where T : new ()
{
}";

            string statementWithoutSpace = @"public void Foo<T>() where T : new()
{
}";

            await this.TestKeywordDeclaration(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", " not", "followed").WithLocation(9, 40);

            await this.TestKeywordDeclaration(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestNewConstructorContraintStatement_MethodWithMultipleConstraints()
        {
            string statementWithSpace = @"public void Foo<T>() where T : IDisposable, new ()
{
}";

            string statementWithoutSpace = @"public void Foo<T>() where T : IDisposable, new()
{
}";

            await this.TestKeywordDeclaration(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", " not", "followed").WithLocation(9, 53);

            await this.TestKeywordDeclaration(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestNewConstructorContraintStatement_MethodWithClassConstraints()
        {
            string statementWithSpace = @"public void Foo<T>() where T : class, new ()
{
}";

            string statementWithoutSpace = @"public void Foo<T>() where T : class, new()
{
}";

            await this.TestKeywordDeclaration(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("new", " not", "followed").WithLocation(9, 47);

            await this.TestKeywordDeclaration(statementWithSpace, expected, statementWithoutSpace);
        }

        [Fact]
        public async Task TestAwaitIdentifier()
        {
            string statementWithoutSpace = @"var result = await(default(Task<int>));";

            string statementWithSpace = @"var result = await (default(Task<int>));";

            await this.TestKeywordStatement(statementWithoutSpace, EmptyDiagnosticResults, statementWithoutSpace, asyncMethod: false);
            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace, asyncMethod: false);
        }

        [Fact]
        public async Task TestAwaitStatement()
        {
            string statementWithoutSpace = @"var result = await(default(Task<int>));";

            string statementWithSpace = @"var result = await (default(Task<int>));";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace, asyncMethod: true);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("await", string.Empty, "followed").WithLocation(12, 26);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace, asyncMethod: true);
        }

        [Fact]
        public async Task TestCaseStatement()
        {
            string statementWithoutSpace = @"switch (3)
{
case(3):
default:
    break;
}
";

            string statementWithSpace = @"switch (3)
{
case (3):
default:
    break;
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("case", string.Empty, "followed").WithLocation(14, 1);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        [Fact]
        public async Task TestGotoCaseStatement()
        {
            string statementWithoutSpace = @"switch (3)
{
case 2:
    goto case(3);

case 3:
default:
    break;
}
";

            string statementWithSpace = @"switch (3)
{
case 2:
    goto case (3);

case 3:
default:
    break;
}
";

            await this.TestKeywordStatement(statementWithSpace, EmptyDiagnosticResults, statementWithSpace);

            DiagnosticResult expected = this.CSharpDiagnostic().WithArguments("case", string.Empty, "followed").WithLocation(15, 10);

            await this.TestKeywordStatement(statementWithoutSpace, expected, statementWithSpace);
        }

        private Task TestKeywordStatement(string statement, DiagnosticResult expected, string fixedStatement, string returnType = "void", bool asyncMethod = false)
        {
            return this.TestKeywordStatement(statement, new[] { expected }, fixedStatement, returnType, asyncMethod);
        }

        private async Task TestKeywordStatement(string statement, DiagnosticResult[] expected, string fixedStatement, string returnType = "void", bool asyncMethod = false)
        {
            string testCodeFormat = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Namespace
{{
    {2}class ClassName
    {{
        {0}{4} Foo()
        {{
            {1}
        }}
        {3}
    }}
}}
";

            string unsafeModifier = asyncMethod ? string.Empty : "unsafe ";
            string asyncModifier = asyncMethod ? "async " : string.Empty;
            string awaitMethod = asyncMethod ? string.Empty : "int await(Task task) { return 0; }";
            string testCode = string.Format(testCodeFormat, asyncModifier, statement, unsafeModifier, awaitMethod, returnType);
            string fixedTest = string.Format(testCodeFormat, asyncModifier, fixedStatement, unsafeModifier, awaitMethod, returnType);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpFixAsync(testCode, fixedTest, cancellationToken: CancellationToken.None);
        }

        private Task TestKeywordDeclaration(string statement, DiagnosticResult expected, string fixedStatement)
        {
            return this.TestKeywordDeclaration(statement, new[] { expected }, fixedStatement);
        }

        private async Task TestKeywordDeclaration(string statement, DiagnosticResult[] expected, string fixedStatement)
        {
            string testCodeFormat = @"
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Namespace
{{
    class ClassName
    {{
        {0}
    }}
}}
";

            string testCode = string.Format(testCodeFormat, statement);
            string fixedTest = string.Format(testCodeFormat, fixedStatement);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpFixAsync(testCode, fixedTest, cancellationToken: CancellationToken.None);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new SA1000CodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new SA1000KeywordsMustBeSpacedCorrectly();
        }
    }
}
