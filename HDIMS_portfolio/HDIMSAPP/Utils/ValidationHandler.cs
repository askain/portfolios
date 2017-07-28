using System;
using System.Collections.Generic;


namespace HDIMSAPP.Utils
{
    public class ValidationHandler
    {
        private Dictionary<string, string> BrokenRules { set; get; }

        public ValidationHandler()
        {
            this.BrokenRules = new Dictionary<string, string>();
        }

        public bool BrokenRuleExists(string property)
        {
            return this.BrokenRules.ContainsKey(property);
        }

        public void RemoveBrokenRule(string property)
        {
            if (this.BrokenRules.ContainsKey(property))
            {
                this.BrokenRules.Remove(property);
            }
        }

        public bool ValidateRule(string property, string message, Func<bool> ruleCheck)
        {
            if (!ruleCheck.Invoke())
            {
                this.BrokenRules.Add(property, message);
                return false;
            }
            this.RemoveBrokenRule(property);
            return true;
        }

        public string this[string property]
        {
            get
            {
                return this.BrokenRules[property];
            }
        }

    }
}
