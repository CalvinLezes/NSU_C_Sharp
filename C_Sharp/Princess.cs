﻿namespace C_Sharp
{
    /// <summary>
    /// Princess who is trying to find a husband.
    /// She will go on a dates with some contenders and will have to choose one.
    /// Her happiness will be determined by a score of a contender she chose to be her husband,
    /// Princess doesn't have access to contenders' scores, but she can ask her Friend to tell her
    /// if one contender is better then the other.
    /// If Princess doesn't choose anybody, her level of happiness will be 10.
    /// </summary>
    class Princess
    {
        /// <summary>
        /// Princess will skip first 100/e contestants
        /// </summary>
        private const int NumberOfContendersToSkip = 36;

        /// <summary>
        /// Husband, who the Princess chose
        /// </summary>
        private bool _iAmSingle = true;

        /// <summary>
        /// Hall, where contenders are.
        /// </summary>
        private readonly Hall _hall;

        /// <summary>
        /// Friend, who Princess uses for help
        /// </summary>
        private readonly Friend _friend;

        public Princess(Hall hall, Friend friend)
        {
            this._hall = hall;
            this._friend = friend;
        }

        /// <summary>
        /// Princess has a date with a contender, and decides if she marries him or not.
        /// </summary>
        /// <param name="contender"></param>
        public void HaveADate(Contender contender)
        {
            if (_hall.Visited.Find(previous => _friend.CompareContenders(contender, previous)) == null)
            {
                _hall.SetHusband(contender);
                _iAmSingle = false;
            }
        }

        /// <summary>
        /// Princess is trying to find a husband.
        /// She is having dates with contenders until she fins a husband.
        /// </summary>
        /// <returns></returns>
        public void FindHusband()
        {
            var numberOfDates = 0;
            while (_iAmSingle && !_hall.IsEmpty())
            {
                var contender = _hall.GetNextContender();
                _hall.Visited.Add(contender);
                if (numberOfDates > NumberOfContendersToSkip) //Princess skips first 100/e contenders
                {
                    HaveADate(contender);
                }
                numberOfDates++;
            }
        }

        /// <summary>
        /// Get Princess' happiness score
        /// </summary>
        /// <returns>Happiness score</returns>
        public int GetHappiness()
        {
            var score = _hall.GetHusbandScore();
            if (score == 0)
            {
                return 10; //If the Princess didn't choose a husband, her happiness score is 10
            }
            return score < 51 ? 0 : score; //If contender's score is less then 51, her happiness is 0
        }
    }
}
