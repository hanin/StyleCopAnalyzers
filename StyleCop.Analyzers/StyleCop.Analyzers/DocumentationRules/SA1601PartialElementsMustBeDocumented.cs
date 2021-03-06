﻿namespace StyleCop.Analyzers.DocumentationRules
{
    using System.Linq;
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using StyleCop.Analyzers.Helpers;

    /// <summary>
    /// A C# partial element is missing a documentation header.
    /// </summary>
    /// <remarks>
    /// <para>C# syntax provides a mechanism for inserting documentation for classes and elements directly into the
    /// code, through the use of XML documentation headers. For an introduction to these headers and a description of
    /// the header syntax, see the following article:
    /// <see href="http://msdn.microsoft.com/en-us/magazine/cc302121.aspx">XML Comments Let You Build Documentation
    /// Directly From Your Visual Studio .NET Source Files</see>.</para>
    ///
    /// <para>A violation of this rule occurs if a partial element (an element with the partial attribute) is completely
    /// missing a documentation header, or if the header is empty. In C# the following types of elements can be
    /// attributed with the partial attribute: classes, methods.</para>
    ///
    /// <para>When documentation is provided on more than one part of the partial class, the documentation for the two
    /// classes may be merged together to form a single source of documentation. For example, consider the following two
    /// parts of a partial class:</para>
    ///
    /// <code language="csharp">
    /// /// &lt;summary&gt;
    /// /// Documentation for the first part of Class1.
    /// /// &lt;/summary&gt;
    /// public partial class Class1
    /// {
    /// }
    ///
    /// /// &lt;summary&gt;
    /// /// Documentation for the second part of Class1.
    /// /// &lt;/summary&gt;
    /// public partial class Class1
    /// {
    /// }
    /// </code>
    ///
    /// <para>These two different parts of the same partial class each provide different documentation for the class.
    /// When the documentation for this class is built into an SDK, the tool building the documentation will either
    /// choose to use only one part of the documentation for the class and ignore the other parts, or, in some cases, it
    /// may merge the two sources of documentation together, to form a string like: "Documentation for the first part of
    /// Class1. Documentation for the second part of Class1."</para>
    ///
    /// <para>For these reasons, it can be problematic to provide SDK documentation on more than one part of the partial
    /// class. However, it is still advisable to document each part of the class, to increase the readability and
    /// maintainability of the code, and StyleCop will require that each part of the class contain header
    /// documentation.</para>
    ///
    /// <para>This problem is solved through the use of the <c>&lt;content&gt;</c> tag, which can replace the
    /// <c>&lt;summary&gt;</c> tag for partial classes. The recommended practice for documenting partial classes is to
    /// provide the official SDK documentation for the class on the main part of the partial class. This documentation
    /// should be written using the standard <c>&lt;summary&gt;</c> tag. All other parts of the partial class should
    /// omit the <c>&lt;summary&gt;</c> tag completely, and replace it with a <c>&lt;content&gt;</c> tag. This allows
    /// the developer to document all parts of the partial class while still centralizing all of the official SDK
    /// documentation for the class onto one part of the class. The <c>&lt;content&gt;</c> tags will be ignored by the
    /// SDK documentation tools.</para>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1601PartialElementsMustBeDocumented : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID for diagnostics produced by the <see cref="SA1601PartialElementsMustBeDocumented"/> analyzer.
        /// </summary>
        public const string DiagnosticId = "SA1601";
        private const string Title = "Partial elements must be documented";
        private const string MessageFormat = "Partial elements must be documented";
        private const string Category = "StyleCop.CSharp.DocumentationRules";
        private const string Description = "A C# partial element is missing a documentation header.";
        private const string HelpLink = "http://www.stylecop.com/docs/SA1601.html";

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
            context.RegisterSyntaxNodeAction(this.HandleTypeDeclaration, SyntaxKind.ClassDeclaration);
            context.RegisterSyntaxNodeAction(this.HandleTypeDeclaration, SyntaxKind.InterfaceDeclaration);
            context.RegisterSyntaxNodeAction(this.HandleTypeDeclaration, SyntaxKind.StructDeclaration);
            context.RegisterSyntaxNodeAction(this.HandleMethodDeclaration, SyntaxKind.MethodDeclaration);
        }

        private void HandleTypeDeclaration(SyntaxNodeAnalysisContext context)
        {
            TypeDeclarationSyntax typeDeclaration = context.Node as TypeDeclarationSyntax;
            if (typeDeclaration != null)
            {
                if (typeDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
                {
                    if (!XmlCommentHelper.HasDocumentation(typeDeclaration))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Descriptor, typeDeclaration.Identifier.GetLocation()));
                    }
                }
            }
        }

        private void HandleMethodDeclaration(SyntaxNodeAnalysisContext context)
        {
            MethodDeclarationSyntax methodDeclaration = context.Node as MethodDeclarationSyntax;
            if (methodDeclaration != null)
            {
                if (methodDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
                {
                    if (!XmlCommentHelper.HasDocumentation(methodDeclaration))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(Descriptor, methodDeclaration.Identifier.GetLocation()));
                    }
                }
            }
        }
    }
}
