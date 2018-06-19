using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MidiAnlayzer : MonoBehaviour {
    public string filename;

    public GameObject greenNote;
    public GameObject redNote;
    public GameObject yellowNote;
    public GameObject blueNote;
    public GameObject orangeNote;

    private static GameObject[] noteObjects = new GameObject[5];

public GameObject getIdealString(int note)
    {
        /*Debug.Log("Note : " + note);
        int[] strings = new int[] { 64, 69, 74, 79, 83 };
        if (note < strings[0]) return null;

        int idealString = 0;
        for (int x = 1; x < strings.Length; x++)
        {
            if (note < strings[x]) break;
            idealString = x;
        }*/

        int noteMod = (note - 21) % 12;
        if (noteMod == 0 || noteMod == 1) // A
            return noteObjects[0];
        if (noteMod == 5 || noteMod == 6) // D
            return noteObjects[1];
        if (noteMod == 9 || noteMod == 10 || noteMod == 11) // G
            return noteObjects[2];
        if (noteMod == 2 || noteMod == 3 || noteMod == 4) // B
            return noteObjects[3];
        if (noteMod == 7 || noteMod == 8) // E
            return noteObjects[4];

        return null;
    }


    // Use this for initialization
    void Start () {
        noteObjects[0] = greenNote;
        noteObjects[1] = redNote;
        noteObjects[2] = yellowNote;
        noteObjects[3] = blueNote;
        noteObjects[4] = orangeNote;

        MidiFile midiFile = MidiFile.Read(Application.dataPath + "/Resources/" + this.filename);
        IEnumerable<Melanchall.DryWetMidi.Smf.Interaction.Note> notes = midiFile.GetNotes();

        TempoMap tempoMap = midiFile.GetTempoMap();
        foreach (Melanchall.DryWetMidi.Smf.Interaction.Note note in notes)
        {
            GameObject noteObj = getIdealString(note.NoteNumber);
            if (noteObj == null) continue;

            MetricTime metricTime = note.TimeAs<MetricTime>(tempoMap);
            MetricLength metricLength = note.LengthAs<MetricLength>(tempoMap);

            double start = (16.3 + metricTime.TotalMicroseconds * Math.Pow(10, -6)) * 25 * Time.deltaTime;
            double length = metricLength.TotalMicroseconds * Math.Pow(10, -6) * 25 * Time.deltaTime;

            Vector3 location = new Vector3(noteObj.transform.localPosition.x, noteObj.transform.localPosition.y, 0.5f + (float) start);
            GameObject noteObject = Instantiate(noteObj, location, Quaternion.identity, this.transform);
            noteObject.transform.localPosition = location;
            noteObject.transform.localRotation = noteObj.transform.localRotation;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
