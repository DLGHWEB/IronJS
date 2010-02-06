﻿using System;
using System.Text;
using Antlr.Runtime.Tree;
using IronJS.Runtime2.Js;

#if CLR2
using Microsoft.Scripting.Ast;
#else
using System.Linq.Expressions;
#endif

namespace IronJS.Compiler.Ast
{
    using AstUtils = Microsoft.Scripting.Ast.Utils;
    using Et = Expression;
    using System.Collections.Generic;

    public class For : Node
    {
        public INode Setup { get; protected set; }
        public INode Test { get; protected set; }
        public INode Incr { get; protected set; }
        public INode Body { get; protected set; }

        public For(INode setup, INode test, INode incr, INode body, ITree node)
            : base(NodeType.ForStep, node)
        {
            Setup = setup;
            Test = test;
            Incr = incr;
            Body = body;
        }

        public override INode Analyze(Stack<Function> astopt)
        {
            if (Setup != null)
                Setup = Setup.Analyze(astopt);

            if(Test != null)
                Test = Test.Analyze(astopt);

            if (Incr != null)
                Incr = Incr.Analyze(astopt);

            if (Body != null)
                Body = Body.Analyze(astopt);

            return this;
        }

        public static ParameterExpression TMP = Et.Variable(typeof(object), "__tmp__");
        public static ParameterExpression TST = Et.Variable(typeof(object), "__tst__");

        public override Et Compile(Function func)
        {
            Et test = AstUtils.Empty();
            Et setup = AstUtils.Empty();
            Et incr = AstUtils.Empty();

            if (Setup != null)
                setup = Setup.Compile(func);

            if (Test != null)
                if (Test.Type == IjsTypes.Boolean)
                    test = Test.Compile(func);
                else
                    throw new NotImplementedException();

            else
                test = Et.Constant(true, typeof(bool));

            if (Incr != null)
                incr = Incr.Compile(func);

            return Et.Block(
                new[] { TMP, TST },
                setup,
                AstUtils.Loop(
                    test,
                    incr,
                    Body.Compile(func),
                    AstUtils.Empty()
                )
            );
        }

        public override void Write(StringBuilder writer, int indent)
        {
            string indentStr = new String(' ', indent * 2);

            writer.AppendLine(indentStr + "(" + NodeType);

            Setup.Write(writer, indent + 1);
            Test.Write(writer, indent + 1);
            Incr.Write(writer, indent + 1);
            Body.Write(writer, indent + 1);

            writer.AppendLine(indentStr + ")");
        }
    }
}
