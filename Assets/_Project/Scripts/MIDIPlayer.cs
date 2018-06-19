using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;
using System.IO;
using System;

[RequireComponent(typeof(AudioSource))]
public class MIDIPlayer : MonoBehaviour
{
	//Public
	//Check the Midi's file folder for different songs
	public string midiFilePath;
    public bool ShouldPlayFile = true;

    //Try also: "FM Bank/fm" or "Analog Bank/analog" for some different sounds
    public string bankFilePath = "GM Bank/gm";
    public int bufferSize = 1024;
    public int midiNote = 60;
    public int midiNoteVolume = 100;
    [Range(0, 127)] //From Piano to Gunshot
    public int midiInstrument = 0;

    //Private 
    private float[] sampleBuffer;
    private float gain = 1f;
    private MidiSequencer midiSequencer;
    private StreamSynthesizer midiStreamSynthesizer;

    private float sliderValue = 1.0f;
    private float maxSliderValue = 127.0f;

	// - Ours:
	private bool[] tracks = new bool[5];
	private GameObject[] notes = new GameObject[5];
    
    public GameObject greenNote;
	public GameObject redNote;
	public GameObject yellowNote;
	public GameObject blueNote;
	public GameObject orangeNote;

    private SortedDictionary<int, int> midiToNote;

    // Awake is called when the script instance
    // is being loaded.
    void Awake()
    {
        midiStreamSynthesizer = new StreamSynthesizer(44100, 2, bufferSize, 40);
        sampleBuffer = new float[midiStreamSynthesizer.BufferSize];
        
        midiStreamSynthesizer.LoadBank(bankFilePath);
       
        midiSequencer = new MidiSequencer(midiStreamSynthesizer);

        //These will be fired by the midiSequencer when a song plays. Check the console for messages if you uncomment these
        midiSequencer.NoteOnEvent += new MidiSequencer.NoteOnEventHandler (MidiNoteOnHandler);
        midiSequencer.NoteOffEvent += new MidiSequencer.NoteOffEventHandler (MidiNoteOffHandler);			
    }

    void LoadSong(string midiPath)
    {
        
        midiSequencer.LoadMidi(midiPath, false);    
        Invoke("Play", 16.5f);

    }

    void Play()
    {
        midiSequencer.Play();
    }

    // Start is called just before any of the
    // Update methods is called the first time.
    void Start()
    {
        notes[0] = greenNote;
		notes[1] = redNote;
		notes[2] = yellowNote;
		notes[3] = blueNote;
		notes[4] = orangeNote;

        midiToNote = new SortedDictionary<int, int>();
        for (int i = 21; i <= 108; ++i)
        {
            int noteMod = (i - 21) % 12;
            if (noteMod == 0 || noteMod == 1) // A
                midiToNote.Add(i, 0);
            if (noteMod == 5 || noteMod == 6) // D
                midiToNote.Add(i, 1);
            if (noteMod == 9 || noteMod == 10 || noteMod == 11) // G
                midiToNote.Add(i, 2);
            if (noteMod == 2 || noteMod == 3 || noteMod == 4) // B
                midiToNote.Add(i, 3);
            if (noteMod == 7 || noteMod == 8) // E
                midiToNote.Add(i, 4);
        }
    }

    // Update is called every frame, if the
    // MonoBehaviour is enabled.
    void Update()
    {
        if (!midiSequencer.isPlaying)
        {
            if (ShouldPlayFile)
            {
                LoadSong(midiFilePath);
            }
        }
        else if (!ShouldPlayFile)
        {
            midiSequencer.Stop(true);
        }

        for (int track = 0; track < tracks.Length; ++track)
		{
			if (tracks[track])
			{
                Vector3 location = new Vector3(notes[track].transform.localPosition.x, notes[track].transform.localPosition.y, 0.5f);
                GameObject noteObject = Instantiate(notes[track], location, Quaternion.identity, this.transform);
                noteObject.transform.localPosition = location;
                noteObject.transform.localRotation = notes[track].transform.localRotation;
                Destroy(noteObject, 2.1f);
			}
		}
        
		tracks[0] = tracks[1] = tracks[2] = tracks[3] = tracks[4] = false; 
    }
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        //This uses the Unity specific float method we added to get the buffer
        midiStreamSynthesizer.GetNext(sampleBuffer);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = sampleBuffer[i] * gain;
        }
    }

    public void MidiNoteOnHandler(int channel, int note, int velocity)
    {
		setNote(note, true);
    }

    public void MidiNoteOffHandler(int channel, int note)
    {
        setNote(note, false);
        //Debug.Log("NoteOff: " + note.ToString());
    }

	public void setNote(int note, bool value) 
	{
        tracks[midiToNote[note]] = value;
	}
}
