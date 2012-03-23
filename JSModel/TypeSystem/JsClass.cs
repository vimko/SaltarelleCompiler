﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Saltarelle.Compiler.JSModel;
using Saltarelle.Compiler.JSModel.Expressions;
using Saltarelle.Compiler.JSModel.Statements;

namespace Saltarelle.Compiler.JSModel.TypeSystem {
    /// <summary>
    /// A class, interface or struct.
    /// </summary>
    public class JsClass : JsType {
        private JsFunctionDefinitionExpression _unnamedConstructor;

        public enum ClassTypeEnum { Struct, Class, Interface }

        public ClassTypeEnum ClassType { get; private set; }
        public JsConstructedType BaseClass { get; private set; }
        public IList<string> TypeArgumentNames { get; private set; }
        public IList<JsConstructedType> ImplementedInterfaces { get; private set; }
        public IList<JsNamedConstructor> NamedConstructors { get; private set; }
        public IList<JsMethod> InstanceMethods { get; private set; }
        public IList<JsMethod> StaticMethods { get; private set; }
        public IList<JsStatement> InstanceInitStatements { get; private set; }
        public IList<JsStatement> StaticInitStatements { get; private set; }

        public JsFunctionDefinitionExpression UnnamedConstructor {
            get { return _unnamedConstructor; }
            set {
                if (Frozen)
                    throw new InvalidOperationException("Object is frozen.");
                if (_unnamedConstructor != null)
                    throw new InvalidOperationException("Can only set the unnamed constructor once.");
                _unnamedConstructor = value;
            }
        }

        public JsClass(ScopedName name, ClassTypeEnum classType, IEnumerable<string> typeArgumentNames, JsConstructedType baseClass, IEnumerable<JsConstructedType> implementedInterfaces) : base(name) {
            BaseClass              = baseClass;
            ClassType              = classType;
            TypeArgumentNames      = new List<string>(typeArgumentNames ?? new string[0]);
            ImplementedInterfaces  = new List<JsConstructedType>(implementedInterfaces ?? new JsConstructedType[0]);
            NamedConstructors      = new List<JsNamedConstructor>();
            InstanceMethods        = new List<JsMethod>();
            StaticMethods          = new List<JsMethod>();
            InstanceInitStatements = new List<JsStatement>();
            StaticInitStatements   = new List<JsStatement>();
        }

        public override void Freeze() {
            base.Freeze();
            
            TypeArgumentNames      = TypeArgumentNames.AsReadOnly();
            ImplementedInterfaces  = ImplementedInterfaces.AsReadOnly();
            NamedConstructors      = NamedConstructors.AsReadOnly();
            InstanceMethods        = InstanceMethods.AsReadOnly();
            StaticMethods          = StaticMethods.AsReadOnly();
            InstanceInitStatements = InstanceInitStatements.AsReadOnly();
            StaticInitStatements   = StaticInitStatements.AsReadOnly();
        }
    }
}
