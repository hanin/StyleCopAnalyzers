﻿namespace StyleCop.Analyzers.NamingRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// There are currently no situations in which this rule will fire.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    [NoCodeFix("Don't fix what isn't broken.")]
    public class SA1301ElementMustBeginWithLowerCaseLetter : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID for diagnostics produced by the <see cref="SA1301ElementMustBeginWithLowerCaseLetter"/> analyzer.
        /// </summary>
        public const string DiagnosticId = "SA1301";
        private const string Title = "Element must begin with lower-case letter";
        private const string MessageFormat = "Element must begin with lower-case letter";
        private const string Category = "StyleCop.CSharp.NamingRules";
        private const string Description = "There are currently no situations in which this rule will fire.";
        private const string HelpLink = "http://www.stylecop.com/docs/SA1301.html";

        private static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true, Description, HelpLink);

        private static readonly ImmutableArray<DiagnosticDescriptor> SupportedDiagnosticsValue =
            ImmutableArray.Create(Descriptor);

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return SupportedDiagnosticsValue;
            }
        }

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            // Intentionally empty
        }
    }
}
