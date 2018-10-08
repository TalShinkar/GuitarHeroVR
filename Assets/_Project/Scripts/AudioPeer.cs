using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour {
    static int FREQ_NUM = 5;
    static int NUM_SAMPLES = 1024;


    AudioSource _audioSource;
    public static float[] _samples = new float[NUM_SAMPLES];
    // for better visualisation, work with 8 frequencies bands
    public static float[] _freqBand = new float[FREQ_NUM];
    // every time the freqBand becomes higher then the bandBuffer it will become it(also if lower)
    public static float[] _bandBuffer = new float[FREQ_NUM];
    float[] _bufferDecrease = new float[FREQ_NUM];

    //we want to split the volume between 0-1
    float[] _freqBandHighest = new float[FREQ_NUM];
    public static float[] _audioBand = new float[FREQ_NUM];
    public static float[] _audioBandBuffer = new float[FREQ_NUM];

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }

    void GetSpectrumAudioSource()
    {
        // There are two good options here: Blackman and BlackmanHarris
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands() {
        /* we have 512 samples and we want to split into 8 frequencies bands
        * In the tutorial the song is 22050Hz / 512 = 43Hz per sample
        * The bands:
        * 0 - 2 = 86Hz
        * 1 - 4 = 172Hz
        * 2 - 8 = 344Hz
        * 3 - 16 = 688Hz
        * 4 - 32 = 1376Hz
        * 5 - 64 = 2752Hz
        * 6 - 128 = 5504Hz
        * 7 - 256 = 11008Hz
        * Total of 510 samples
         */



        /* we have 1024 samples and we want to split into 5 frequencies bands
       * In the tutorial the song is 22050Hz / 512 = 43Hz per sample
       * The bands:
       * 0 - 4 = 86Hz
       * 1 - 16 = 172Hz
       * 2 - 64 = 344Hz
       * 3 - 256 = 688Hz
       * 4 - 512 = 1376Hz (+ 172)
       * Total of 852 samples
        */
        int count = 0;
        for (int i = 0; i < FREQ_NUM; i++)
        {

            // calculate the avg amplitude of all samples combined
            float avg = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 4)
            {
                sampleCount += 172;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                avg += _samples[count] * (count + 1);
                count++;
            }
            avg /= count;

            _freqBand[i] = avg * 10; // so it won't be too small
        }

    }

    void BandBuffer()
    {
        for (int i = 0; i < FREQ_NUM; i++) {
            if (_freqBand[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBand[i];
                _bufferDecrease[i] = 0.005f;
            }

            if (_freqBand[i] < _bandBuffer[i])
            {
                _bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f; // higher so it will fall down faster
            }

        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < FREQ_NUM; i++)
        {
            _freqBand[i] = Mathf.Abs(_freqBand[i]);
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }
}