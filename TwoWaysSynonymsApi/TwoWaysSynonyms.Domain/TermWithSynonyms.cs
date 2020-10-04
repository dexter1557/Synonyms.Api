namespace TwoWaysSynonyms.Domain
{
    public class TermWithSynonyms
    {
        public string Term { get; set; }
        public string[] Synonyms { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TermWithSynonyms))
                return false;
            else
            {
                var itemToCompare = obj as TermWithSynonyms;
                if (itemToCompare.Term == this.Term && CompareArrays(itemToCompare.Synonyms))
                    return true;
                else
                    return false;
            }
        }
        private bool CompareArrays(string[] arrayToCompare)
        {
            if (this.Synonyms.Length != arrayToCompare.Length)
                return false;
            for (int i = 0; i < this.Synonyms.Length; i++)
            {
                if (this.Synonyms[i] != arrayToCompare[i])
                    return false;
            }
            return true;
        }
    }
}
