﻿using System;
using System.Text;
using Antlr.Runtime.Tree;
using IronJS.Runtime;
using IronJS.Runtime2.Js;
using System.Collections.Generic;
using AstUtils = Microsoft.Scripting.Ast.Utils;

#if CLR2
using Microsoft.Scripting.Ast;
#else
using System.Linq.Expressions;
#endif

namespace IronJS.Compiler.Ast
{
    public class Throw : Node
    {
        public INode Value { get; protected set; }

        public Throw(INode value, ITree node)
            : base(NodeType.Throw, node)
        {
            Value = value;
        }

        public override INode Analyze(Stack<Function> astopt)
        {
            Value = Value.Analyze(astopt);
            return this;
        }

        public override void Write(StringBuilder writer, int indent)
        {
            string indentStr = new String(' ', indent * 2);

            writer.AppendLine(indentStr + "(" + NodeType);

            if (Value != null)
                Value.Write(writer, indent + 1);

            writer.AppendLine(indentStr + ")");
        }
    }
}
