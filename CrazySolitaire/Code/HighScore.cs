using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazySolitaire;
[Serializable()]
public class HighScores
{
    public int Score { get; set; }
    public List<HighScores> ScoreList { get; set; }

    public HighScores() {
       ScoreList = new List<HighScores>();
    }

    public void AddScore(int score)
    {
        Score = score;
        ScoreList.Add(this);
        ScoreList = ScoreList.OrderByDescending(s => s.Score).ToList();
        if (ScoreList.Count > 11) {
            ScoreList.RemoveAt(ScoreList.Count - 1);
        }
    }


}

