using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JScrambler.Client
{
    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }

    // TODO: move this to Enums
    public enum BrowserOsLock
    {
        Disabled,

        [StringValue("firefox")]
        FirefoxBrowser = 101,

        [StringValue("chrome")]
        ChromeBrowser = 102,

        [StringValue("iexplorer")]
        InternetExplorer = 103,

        [StringValue("linux")]
        Linux = 201,

        [StringValue("windows")]
        Windows = 202,

        [StringValue("mac_os")]
        MacOS = 203,

        [StringValue("tizen")]
        Tizen = 301,

        [StringValue("android")]
        Android = 302,

        [StringValue("ios")]
        iOS = 303
    }

    public enum RenameLocal
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum RenameAll
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum ConstantFolding
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum DeadCode
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum DeadCodeElimination
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum WhitespaceRemoval
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum DuplicateLiterals
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum DictionaryCompression
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum FunctionOutlining
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum FunctionReorder
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum DotNotationElimination
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
    }

    public enum LiteralHooking
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,

        [StringValue("{0};{1};{2}")] // replace by predicates min;max[;percentage]
        Custom,
    }

    public struct LiteralHookingPredicates
    {
        public int Min;
        public int Max;
        public double Percent;

        public LiteralHookingPredicates(int min, int max, double percent)
        {
            this.Min = min;
            this.Max = max;
            this.Percent = percent;
        }
    }

    public enum StringSplitting
    {
        Disabled,

        [StringValue("%DEFAULT%")]
        Enabled,
       
        Occurrences, // 0.01 to 1

        Concatenation // 0 to 1
    }

    

    public class OptionalParameters
    {
        /* Protection code transformations */

        /// <summary>
        /// Turns statements into new function declarations.
        /// </summary>
        public FunctionOutlining FunctionOutlining { get; set; }

        /// <summary>
        /// Randomly reorders your source code's function declarations.
        /// </summary>
        public FunctionReorder FunctionReorder { get; set; }

        /// <summary>
        /// Transforms dot notation to subscript notation.
        /// </summary>
        public DotNotationElimination DotNotationElimination { get; set; }

        private DeadCode deadCode = DeadCode.Disabled;

        /// <summary>
        /// Randomly injects dead code into the source code.
        /// </summary>
        public DeadCode DeadCode
        {
            get
            {
                return this.deadCode;
            }

            set
            {
                this.deadCode = value;

                if (this.deadCode == DeadCode.Enabled)
                {
                    this.DeadCodeElimination = DeadCodeElimination.Disabled;
                }
            }
        }

        /// <summary>
        /// Replaces literals by a randomly sized chain of ternary operators.
        /// </summary>
        public LiteralHooking LiteralHooking { get; set; }

        public LiteralHookingPredicates? LiteralHookingPredicates { get; set; }


        public StringSplitting StringSplittin { get; set; }

        // ...


        /* Optimization code transformations */
        private RenameLocal renameLocal = RenameLocal.Disabled;

        /// <summary>
        /// Renames local names only. The best way to replace names without worrying about name dependencies.
        /// </summary>
        public RenameLocal RenameLocal 
        {
            get
            {
                return this.renameLocal;
            }

            set
            {
                this.renameLocal = value;

                if (this.renameLocal != RenameLocal.Disabled)
                {
                    this.RenameAll = RenameAll.Disabled;
                }
            }
        }

        private RenameAll renameAll = RenameAll.Disabled;

        /// <summary>
        /// Renames all identifiers found at your source code.
        /// </summary>
        public RenameAll RenameAll 
        {
            get
            {
                return this.renameAll;
            }

            set
            {
                this.renameAll = value;

                if (this.renameAll != RenameAll.Disabled)
                {
                    this.RenameLocal = RenameLocal.Disabled;
                }
            }
        }

        /// <summary>
        /// Shrink the size of your JavaScript removing unnecessary whitespaces and newlines from the source code.
        /// </summary>
        public WhitespaceRemoval WhitespaceRemoval { get; set; }

        /// <summary>
        /// Replaces duplicate literals by a symbol.
        /// </summary>
        public DuplicateLiterals DuplicateLiterals { get; set; }

        /// <summary>
        /// Simplifies constant expressions at compile-time to make your code faster at run-time.
        /// </summary>
        public ConstantFolding ConstantFolding { get; set; }

        private DeadCodeElimination deadCodeElimination = DeadCodeElimination.Disabled;

        /// <summary>
        /// Removes dead code and void code from your JavaScript.
        /// </summary>
        public DeadCodeElimination DeadCodeElimination
        {
            get
            {
                return this.deadCodeElimination;
            }

            set
            {
                this.deadCodeElimination = value;

                if (this.deadCodeElimination == DeadCodeElimination.Enabled)
                {
                    this.DeadCode = DeadCode.Disabled;
                }
            }
        }

        /// <summary>
        /// Dictionary compression to shrink even more your source code.
        /// </summary>
        public DictionaryCompression DictionaryCompression { get; set; }

        /* Other code transformations */

        /// <summary>
        /// Set a prefix to be appended to the new names generated by JScrambler. (Other)
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// Define a list of files (relative paths) that JScrambler must ignore. (Other)
        /// </summary>
        public List<string> IgnoreFiles { get; set; }

        /// <summary>
        /// List of exceptions that will never be replaced or used to create new declarations. (Other)
        /// </summary>
        public List<string> ExceptionsList { get; set; }

        /// <summary>
        /// Removes function definitions and function calls with a given name. (Other)
        /// </summary>
        public List<string> AssertsElimination { get; set; }

        /// <summary>
        /// Removes statements and public variable declarations used to control the output of debugging messages that help you debug your code. (Other)
        /// </summary>
        public List<string> DebuggingCodeElimination { get; set; }

        public OptionalParameters()
        {
            this.IgnoreFiles = new List<string>();
            this.ExceptionsList = new List<string>();
            this.AssertsElimination = new List<string>();
            this.DebuggingCodeElimination = new List<string>();
        }

        public SortedDictionary<string, string> GetParameters()
        {
            var parameters = new SortedDictionary<string, string>();

            /* Protection code transformations */

            if (this.FunctionOutlining == FunctionOutlining.Enabled)
            {
                parameters.Add("function_outlining", GetEnumStringValue(this.FunctionOutlining));
            }

            if (this.FunctionReorder == FunctionReorder.Enabled)
            {
                parameters.Add("function_reorder", GetEnumStringValue(this.FunctionReorder));
            }

            if (this.DotNotationElimination == DotNotationElimination.Enabled)
            {
                parameters.Add("dot_notation_elimination", GetEnumStringValue(this.DotNotationElimination));
            }            

            if (this.DeadCode == DeadCode.Enabled)
            {
                parameters.Add("dead_code", GetEnumStringValue(this.DeadCode));
            }

            if (this.LiteralHooking != LiteralHooking.Disabled)
            {
                if (this.LiteralHooking == LiteralHooking.Enabled ||
                    this.LiteralHookingPredicates == null)
                {
                    parameters.Add("literal_hooking", GetEnumStringValue(this.LiteralHooking));
                }
                else
                {
                    var predicatesExpression = string.Format(GetEnumStringValue(this.LiteralHooking),
                        this.LiteralHookingPredicates.Value.Min,
                        this.LiteralHookingPredicates.Value.Max,
                        this.LiteralHookingPredicates.Value.Percent);

                    parameters.Add("literal_hooking", predicatesExpression);
                }
            }

            // string_splitting

            // ...

            /* Optimization code transformations */

            if (this.RenameLocal == RenameLocal.Enabled)
            {
                parameters.Add("rename_local", GetEnumStringValue(this.RenameLocal));
            }

            if (this.RenameAll == RenameAll.Enabled)
            {
                parameters.Add("rename_all", GetEnumStringValue(this.RenameAll));
            }

            if (this.WhitespaceRemoval == WhitespaceRemoval.Enabled)
            {
                parameters.Add("whitespace", GetEnumStringValue(this.WhitespaceRemoval));
            }

            if (this.DuplicateLiterals == DuplicateLiterals.Enabled)
            {
                parameters.Add("duplicate_literals", GetEnumStringValue(this.DuplicateLiterals));
            }

            if (this.ConstantFolding == ConstantFolding.Enabled)
            {
                parameters.Add("constant_folding", GetEnumStringValue(this.ConstantFolding));
            }

            if (this.DeadCodeElimination == DeadCodeElimination.Enabled)
            {
                parameters.Add("dead_code_elimination", GetEnumStringValue(this.DeadCodeElimination));
            }

            if (this.DictionaryCompression == DictionaryCompression.Enabled)
            {
                parameters.Add("dictionary_compression", GetEnumStringValue(this.DictionaryCompression));
            }         

            /* Other code transformations */
            if (!string.IsNullOrEmpty(this.NamePrefix))
            {
                parameters.Add("name_prefix", this.NamePrefix);
            }

            if (this.IgnoreFiles.Count > 0)
            {
                parameters.Add("ignore_files", String.Join(";", this.IgnoreFiles.ToArray()));
            }

            if (this.ExceptionsList.Count > 0)
            {
                parameters.Add("exceptions_list", String.Join(";", this.ExceptionsList.ToArray()));
            }

            if (this.AssertsElimination.Count > 0)
            {
                parameters.Add("asserts_elimination", String.Join(";", this.AssertsElimination.ToArray()));
            }

            if (this.DebuggingCodeElimination.Count > 0)
            {
                parameters.Add("debugging_code_elimination", String.Join(";", this.DebuggingCodeElimination.ToArray()));
            }
            
            return parameters;
        }

        private static string GetEnumStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}