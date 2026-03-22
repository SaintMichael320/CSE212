using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    // -----------------------------
    // Problem 1: FindPairs (O(n))
    // -----------------------------
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> set = new HashSet<string>(words);
        HashSet<string> seen = new HashSet<string>();
        List<string> result = new List<string>();

        foreach (string word in words)
        {
            if (word[0] == word[1]) continue;
            if (seen.Contains(word)) continue;

            string reversed = new string(new char[] { word[1], word[0] });

            if (set.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                seen.Add(word);
                seen.Add(reversed);
            }
        }

        return result.ToArray();
    }

    // -----------------------------
    // Problem 2: SummarizeDegrees
    // -----------------------------
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');

            if (fields.Length < 4)
                continue;

            string degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    // -----------------------------
    // Problem 3: IsAnagram
    // -----------------------------
    public static bool IsAnagram(string word1, string word2)
    {
        string w1 = word1.Replace(" ", "").ToLower();
        string w2 = word2.Replace(" ", "").ToLower();

        if (w1.Length != w2.Length)
            return false;

        Dictionary<char, int> counts = new Dictionary<char, int>();

        foreach (char c in w1)
        {
            if (!counts.ContainsKey(c))
                counts[c] = 0;

            counts[c]++;
        }

        foreach (char c in w2)
        {
            if (!counts.ContainsKey(c))
                return false;

            counts[c]--;

            if (counts[c] < 0)
                return false;
        }

        return true;
    }

    // -----------------------------
    // Problem 5: Earthquake Summary
    // -----------------------------
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var response = client.Send(request);

        using var stream = response.Content.ReadAsStream();
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        List<string> results = new List<string>();

        if (data?.Features != null)
        {
            foreach (var feature in data.Features)
            {
                string place = feature.Properties?.Place ?? "Unknown location";
                double? mag = feature.Properties?.Mag;

                string magText = mag.HasValue ? mag.Value.ToString() : "N/A";

                results.Add($"{place} - Mag {magText}");
            }
        }

        return results.ToArray();
    }
}