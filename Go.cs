
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;
namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF             =  0, // (EOF)
        SYMBOL_ERROR           =  1, // (Error)
        SYMBOL_WHITESPACE      =  2, // Whitespace
        SYMBOL_MINUS           =  3, // '-'
        SYMBOL_LPAREN          =  4, // '('
        SYMBOL_RPAREN          =  5, // ')'
        SYMBOL_TIMES           =  6, // '*'
        SYMBOL_DIV             =  7, // '/'
        SYMBOL_COLON           =  8, // ':'
        SYMBOL_SEMI            =  9, // ';'
        SYMBOL_LBRACE          = 10, // '{'
        SYMBOL_RBRACE          = 11, // '}'
        SYMBOL_PLUS            = 12, // '+'
        SYMBOL_EQ              = 13, // '='
        SYMBOL_CASE            = 14, // case
        SYMBOL_DEFAULT         = 15, // default
        SYMBOL_ELSE            = 16, // else
        SYMBOL_FALSE           = 17, // false
        SYMBOL_FLOAT64         = 18, // 'float64'
        SYMBOL_FOR             = 19, // for
        SYMBOL_ID              = 20, // ID
        SYMBOL_IF              = 21, // if
        SYMBOL_INT             = 22, // int
        SYMBOL_NUMBER          = 23, // Number
        SYMBOL_STRING          = 24, // string
        SYMBOL_SWITCH          = 25, // switch
        SYMBOL_TRUE            = 26, // true
        SYMBOL_VAR             = 27, // var
        SYMBOL_ASSIGN_STMT     = 28, // <assign_stmt>
        SYMBOL_CASE_BLOCK      = 29, // <case_block>
        SYMBOL_CASE_BLOCK_LIST = 30, // <case_block_list>
        SYMBOL_CASE_BLOCKS     = 31, // <case_blocks>
        SYMBOL_DEFAULT_BLOCK   = 32, // <default_block>
        SYMBOL_DEFAULT_OPT     = 33, // <default_opt>
        SYMBOL_EMPTY           = 34, // <empty>
        SYMBOL_EXPR            = 35, // <expr>
        SYMBOL_EXPR_STMT       = 36, // <expr_stmt>
        SYMBOL_FACTOR          = 37, // <factor>
        SYMBOL_FOR_STMT        = 38, // <for_stmt>
        SYMBOL_IF_STMT         = 39, // <if_stmt>
        SYMBOL_PROGRAM         = 40, // <program>
        SYMBOL_STATEMENT       = 41, // <statement>
        SYMBOL_STATEMENTS      = 42, // <statements>
        SYMBOL_SWITCH_STMT     = 43, // <switch_stmt>
        SYMBOL_TERM            = 44, // <term>
        SYMBOL_TYPE            = 45, // <type>
        SYMBOL_VAR_DECL        = 46  // <var_decl>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                                     =  0, // <program> ::= <statements>
        RULE_STATEMENTS                                  =  1, // <statements> ::= <statement> <statements>
        RULE_STATEMENTS2                                 =  2, // <statements> ::= <empty>
        RULE_STATEMENT                                   =  3, // <statement> ::= <var_decl>
        RULE_STATEMENT2                                  =  4, // <statement> ::= <assign_stmt>
        RULE_STATEMENT3                                  =  5, // <statement> ::= <if_stmt>
        RULE_STATEMENT4                                  =  6, // <statement> ::= <for_stmt>
        RULE_STATEMENT5                                  =  7, // <statement> ::= <switch_stmt>
        RULE_STATEMENT6                                  =  8, // <statement> ::= <expr_stmt>
        RULE_VAR_DECL_VAR_ID_EQ_SEMI                     =  9, // <var_decl> ::= var ID <type> '=' <expr> ';'
        RULE_TYPE_INT                                    = 10, // <type> ::= int
        RULE_TYPE_FLOAT64                                = 11, // <type> ::= 'float64'
        RULE_TYPE_STRING                                 = 12, // <type> ::= string
        RULE_ASSIGN_STMT_ID_EQ_SEMI                      = 13, // <assign_stmt> ::= ID '=' <expr> ';'
        RULE_EXPR_STMT_SEMI                              = 14, // <expr_stmt> ::= <expr> ';'
        RULE_EXPR_PLUS                                   = 15, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                  = 16, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                        = 17, // <expr> ::= <term>
        RULE_TERM_TIMES                                  = 18, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                    = 19, // <term> ::= <term> '/' <factor>
        RULE_TERM                                        = 20, // <term> ::= <factor>
        RULE_FACTOR_LPAREN_RPAREN                        = 21, // <factor> ::= '(' <expr> ')'
        RULE_FACTOR_ID                                   = 22, // <factor> ::= ID
        RULE_FACTOR_NUMBER                               = 23, // <factor> ::= Number
        RULE_FACTOR_TRUE                                 = 24, // <factor> ::= true
        RULE_FACTOR_FALSE                                = 25, // <factor> ::= false
        RULE_IF_STMT_IF_LBRACE_RBRACE                    = 26, // <if_stmt> ::= if <expr> '{' <statements> '}'
        RULE_IF_STMT_IF_LBRACE_RBRACE_ELSE_LBRACE_RBRACE = 27, // <if_stmt> ::= if <expr> '{' <statements> '}' else '{' <statements> '}'
        RULE_FOR_STMT_FOR_LBRACE_RBRACE                  = 28, // <for_stmt> ::= for <expr> '{' <statements> '}'
        RULE_SWITCH_STMT_SWITCH_LBRACE_RBRACE            = 29, // <switch_stmt> ::= switch <expr> '{' <case_blocks> '}'
        RULE_CASE_BLOCKS                                 = 30, // <case_blocks> ::= <case_block_list> <default_opt>
        RULE_CASE_BLOCK_LIST                             = 31, // <case_block_list> ::= <case_block> <case_block_list>
        RULE_CASE_BLOCK_LIST2                            = 32, // <case_block_list> ::= <case_block>
        RULE_DEFAULT_OPT                                 = 33, // <default_opt> ::= <default_block>
        RULE_DEFAULT_OPT2                                = 34, // <default_opt> ::= <empty>
        RULE_CASE_BLOCK_CASE_COLON                       = 35, // <case_block> ::= case <expr> ':' <statements>
        RULE_DEFAULT_BLOCK_DEFAULT_COLON                 = 36, // <default_block> ::= default ':' <statements>
        RULE_EMPTY                                       = 37  // <empty> ::= 
    };

    public class MyParser
    {
        private LALRParser parser;
		ListBox lst;
        public MyParser(string filename ,ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
        	this.lst=lst;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FALSE :
                //false
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT64 :
                //'float64'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //ID
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //Number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VAR :
                //var
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN_STMT :
                //<assign_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_BLOCK :
                //<case_block>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_BLOCK_LIST :
                //<case_block_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_BLOCKS :
                //<case_blocks>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT_BLOCK :
                //<default_block>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT_OPT :
                //<default_opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EMPTY :
                //<empty>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR_STMT :
                //<expr_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTS :
                //<statements>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STMT :
                //<switch_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VAR_DECL :
                //<var_decl>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<program> ::= <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS :
                //<statements> ::= <statement> <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTS2 :
                //<statements> ::= <empty>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<statement> ::= <var_decl>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<statement> ::= <assign_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<statement> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<statement> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<statement> ::= <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<statement> ::= <expr_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VAR_DECL_VAR_ID_EQ_SEMI :
                //<var_decl> ::= var ID <type> '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_INT :
                //<type> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_FLOAT64 :
                //<type> ::= 'float64'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_STRING :
                //<type> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_STMT_ID_EQ_SEMI :
                //<assign_stmt> ::= ID '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_STMT_SEMI :
                //<expr_stmt> ::= <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN :
                //<factor> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_ID :
                //<factor> ::= ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_NUMBER :
                //<factor> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TRUE :
                //<factor> ::= true
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_FALSE :
                //<factor> ::= false
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LBRACE_RBRACE :
                //<if_stmt> ::= if <expr> '{' <statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<if_stmt> ::= if <expr> '{' <statements> '}' else '{' <statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LBRACE_RBRACE :
                //<for_stmt> ::= for <expr> '{' <statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LBRACE_RBRACE :
                //<switch_stmt> ::= switch <expr> '{' <case_blocks> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_BLOCKS :
                //<case_blocks> ::= <case_block_list> <default_opt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_BLOCK_LIST :
                //<case_block_list> ::= <case_block> <case_block_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_BLOCK_LIST2 :
                //<case_block_list> ::= <case_block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT_OPT :
                //<default_opt> ::= <default_block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT_OPT2 :
                //<default_opt> ::= <empty>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_BLOCK_CASE_COLON :
                //<case_block> ::= case <expr> ':' <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT_BLOCK_DEFAULT_COLON :
                //<default_block> ::= default ':' <statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EMPTY :
                //<empty> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" In line: "+args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2="Expected token: "+args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

    }
}
