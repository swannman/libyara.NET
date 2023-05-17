using libyaraNET;
using System.ComponentModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class describe_Compiler
    {
        private Compiler _compiler;

        [TestInitialize]
        public void before_each()
        {
            _compiler = new Compiler();
        }

        [TestCleanup]
        public void after_each()
        {
            _compiler.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationException))]
        public void given_invalid_rule_add_rule_string_should_throw()
        {
            _compiler.AddRuleString("invalid rule");
        }

        [TestMethod]
        public void given_valid_rule_file_add_rule_file_should_not_throw()
        {
            _compiler.AddRuleFile(".\\Content\\BasicRule.yara");
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void given_missing_rule_file_add_rule_file_should_throw()
        {
            _compiler.AddRuleFile("c:\\notfound.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(CompilationException))]
        public void given_invalid_rule_file_add_rule_file_should_throw()
        {
            _compiler.AddRuleFile(".\\Content\\InvalidRule.yara");
        }

        [TestMethod]
        public void compiler_exception_should_have_error_messages()
        {
            try
            {
                _compiler.AddRuleString("rule test { bad }");
            }
            catch (CompilationException cex)
            {
                Assert.AreEqual(1, cex.Errors.Count);
                Assert.AreEqual(
                    "syntax error, unexpected hex string, expecting '{' on line 1 in file: [none]", cex.Errors[0]);

                return;
            }

            Assert.Fail("Expected invalid rule to throw.");
        }

        [TestMethod]
        public void given_hash_rule_it_should_compile()
        {
            // there was a breaking change that caused libyara to be compiled
            // without cuckoo or hash module support, this is to catch that

            _compiler.AddRuleString("import \"hash\" rule test { condition: hash.md5(0, 100) == \"abc\"}");
        }

        [TestMethod]
        public void given_file_of_combined_rules_it_should_compile()
        {
            _compiler.AddRuleFile(".\\Content\\CombinedRules.yara");
        }
    }
}
