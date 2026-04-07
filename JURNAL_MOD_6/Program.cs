using System.ComponentModel;
using System.Text.RegularExpressions;

public class MyMusicTrack
{
    private int _id;
    public int PlayCount { get; private set; }
    public string Title { get; private set; }

    private static Random _rnd = new Random();

    public MyMusicTrack(string title)
    {
        Title = title;
        _id = _rnd.Next(1000, 99999);
        PlayCount = 0;
    }

    public void IncreasePlay(int playCount)
    {
        PlayCount += playCount;
    }

    public void PrintTrackDetails()
    {
        Console.WriteLine($"id-{_id}: {Title}, play_count {PlayCount}");
    }
}

public class MyUserMusic
{
    private int _id;
    public string UserName { get; private set; }
    private List<MyMusicTrack> _uploadedTracks = new();

    private static Random _rnd = new Random();

    public MyUserMusic(string userName)
    {
        UserName = userName;
        _id = _rnd.Next(1000, 99999);
    }

    public int GetTotalPlayCount()
    {
        int totalPlayCount = 0;
        foreach (MyMusicTrack track in _uploadedTracks)
        {
            totalPlayCount += track.PlayCount;
        }
        return totalPlayCount;
    }

    public void AddTrack(MyMusicTrack track)
    {
        _uploadedTracks.Add(track);
    }

    public void PrintAllTracks()
    {
        Console.WriteLine($"User-{_id}: {UserName}");

        for (int i = 0; i < _uploadedTracks.Count; ++i)
        {
            Console.Write($"Track {i + 1}, ");
            var track = _uploadedTracks[i];
            track.PrintTrackDetails();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string[] randomTitle = { "Hello", "World", "Test", "123" };
        Random rnd = new Random();
        List<MyMusicTrack> myMusicTracks = new();
        for (int i = 0; i < 10; ++i)
        {
            int idx = rnd.Next(0, randomTitle.Length - 1);
            string title = randomTitle[idx];

            var track = new MyMusicTrack(title);
            myMusicTracks.Add(track);
        }

        MyUserMusic userMusic = new MyUserMusic("Evangelion");
        foreach (var track in myMusicTracks)
        {
            Console.WriteLine($"Review Lagu {track.Title} oleh {userMusic.UserName}");
            userMusic.AddTrack(track);
        }
        userMusic.PrintAllTracks();
    }
}