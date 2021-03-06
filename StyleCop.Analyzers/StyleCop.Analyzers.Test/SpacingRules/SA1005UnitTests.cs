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
    /// Unit test for <see cref="SA1005SingleLineCommentsMustBeginWithSingleSpace"/>
    /// </summary>
    public class SA1005UnitTests : CodeFixVerifier
    {
        /// <summary>
        /// Verifies that the analyzer will properly handle an empty source.
        /// </summary>
        [Fact]
        public async Task TestEmptySource()
        {
            var testCode = string.Empty;
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// Verify that a correct single line comment will not trigger a diagnostic.
        /// </summary>
        [Fact]
        public async Task TestCorrectComment()
        {
            var testCode = @"public class Foo
{
    //
    // Correct comment
    //
    public class Bar
    {
    }
}
";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// Verify that a single line comment without a leading space gets detected and fixed properly.
        /// </summary>
        [Fact]
        public async Task TestNoLeadingSpace()
        {
            var testCode = @"public class Foo
{
    //Wrong comment
    public class Bar
    {
    }
}
";

            var fixedTestCode = @"public class Foo
{
    // Wrong comment
    public class Bar
    {
    }
}
";

            var expected = this.CSharpDiagnostic().WithLocation(3, 5);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpDiagnosticAsync(fixedTestCode, EmptyDiagnosticResults, CancellationToken.None);
            await this.VerifyCSharpFixAsync(testCode, fixedTestCode);
        }

        /// <summary>
        /// Verify that a single line comment with multiple leading spaces gets detected and fixed properly.
        /// </summary>
        [Fact]
        public async Task TestMultipleLeadingSpaces()
        {
            var testCode = @"public class Foo
{
    //   Wrong comment
    public class Bar
    {
    }
}
";

            var fixedTestCode = @"public class Foo
{
    // Wrong comment
    public class Bar
    {
    }
}
";

            var expected = this.CSharpDiagnostic().WithLocation(3, 5);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpDiagnosticAsync(fixedTestCode, EmptyDiagnosticResults, CancellationToken.None);
            await this.VerifyCSharpFixAsync(testCode, fixedTestCode);
        }

        /// <summary>
        /// Verify that a commented code will not trigger a diagnostic.
        /// </summary>
        [Fact]
        public async Task TestCommentedCode()
        {
            var testCode = @"public class Foo
{
    public class Bar
    {
        ////private int a;
////        private int b;
    }
}
";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <inheritdoc/>
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new SA1005SingleLineCommentsMustBeginWithSingleSpace();
        }

        /// <inheritdoc/>
        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new SA1005CodeFixProvider();
        }
    }
}
