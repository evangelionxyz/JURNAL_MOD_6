using System.Diagnostics;

public class MyMusicTrack
{
    public int Id { get; private set; } = -1;
    public int PlayCount { get; private set; } = 0;
    public string? Title { get; private set; }

    private static Random _rnd = new Random();

    public MyMusicTrack(string title)
    {
        try
        {
            if (title == null)
                throw new Exception("Title should not a null");
            if (title.Length <= 0)
                throw new Exception("Title should be a non-empty string");
            if (title.Length > 200)
                throw new Exception("Title length should be less than 200");

            Title = title;
            Id = _rnd.Next(1000, 99999);
            PlayCount = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void IncreasePlay(int playCount)
    {
        try
        {
            if (playCount >= 25_000_000)
                throw new Exception("Play count should be less or equals to 25 million");
            if (int.IsNegative(playCount))
                throw new Exception("Play count should not a negative value");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        try
        {
            checked
            {
                PlayCount += playCount;
            }
        }
        catch (OverflowException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void PrintTrackDetails()
    {
        Console.WriteLine($"id-{Id}: {Title}, play_count {PlayCount}");
    }
}

public class MyUserMusic
{
    public int Id { get; private set; } = -1;
    public string? UserName { get; private set; }
    private List<MyMusicTrack> _uploadedTracks = new();

    private static Random _rnd = new Random();

    public MyUserMusic(string userName)
    {
        try
        {
            if (userName.Length >= 100)
                throw new Exception("Username length should be less than 100");
            if (userName == null)
                throw new Exception("Username should not a null");

            UserName = userName;
            Id = _rnd.Next(1000, 99999);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
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
        try
        {
            if (track == null)
                throw new Exception("Track should not a null");
            if (track.PlayCount >= int.MaxValue)
                throw new Exception("Track play count should not a max integer");
            if (track.Id == -1 || track.Title == null|| (track.Title != null && track.Title.Length == 0)) 
                throw new Exception("Failed to add track");

            _uploadedTracks.Add(track);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void PrintAllTracks()
    {
        Console.WriteLine($"User-{Id}: {UserName}");

        for (int i = 0; i < _uploadedTracks.Count; ++i)
        {
            if (i > 8)
            {
                break;
            }
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
        MyUserMusic userMusic = new MyUserMusic("Evangelion");
        Console.WriteLine("=== Test track title limit to 201 chars ===");
        try
        {
            userMusic.AddTrack(new MyMusicTrack(new string('A', 201)));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("=== Test exception overflow");
        {
            var track = new MyMusicTrack("TEST OVERFLOW");
            for (int i = 0; i < 90; ++i)
            {
                track.IncreasePlay(24_000_000);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("=== Test 25 mill exception");
        {
            var track = new MyMusicTrack("TEST MAX EXCEPTION");
            track.IncreasePlay(26_000_000);
            Console.WriteLine();
        }

        List<MyMusicTrack> myMusicTracks = new();
        for (int i = 0; i < 10; ++i)
        {
            int idx = rnd.Next(0, randomTitle.Length - 1);
            string title = randomTitle[idx];

            var track = new MyMusicTrack(title);
            myMusicTracks.Add(track);
        }

        
        foreach (var track in myMusicTracks)
        {
            Console.WriteLine($"Review Lagu {track.Title} {track.Id} oleh {userMusic.UserName}");
            userMusic.AddTrack(track);
        }
        userMusic.PrintAllTracks();
    }
}