using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
    Author: Evan Otero, Drew Hoo, and Will Bowdtich
    Date:   April 26, 2017
    
    State
    
    ***********************
    ******* LICENSE *******
    ***********************
    JoyceStick is a Boston College digital humanities project employing Unity
    to construct a virtual reality game from Joyce’s Ulysses for viewing on the
    HTC Vive, supported by a Teaching and Mentoring Grant and substantial funding
    from internal bodies at Boston College.
    Copyright (C) 2017  Evan Otero, Drew Hoo, Emaad Ali, Will Bowditch, Matt Harty, Jake Schafer, & Ryan Reede
    http://joycestick.bc.edu/

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private IState INTRODUCTION, TASK1, TASK2, COMPLETE;

    private IState currentState;

    private void Start() {
        // Initialize States
        INTRODUCTION.Init(this);
        TASK1.Init(this);
        TASK2.Init(this);
        COMPLETE.Init(this);

        // Start Game
        setState(getIntroduction());
    }

    private void Update() { }

    /* Actions */
    public void loadLevel(string sceneName, bool userActivated) {
        StartCoroutine(SceneLoader.instance.AsyncLoadScene(sceneName, userActivated));
    }

    public void playAudio(AudioClip clip, AudioSource source, float volumeScale) {
        source.PlayOneShot(clip, volumeScale);
    }

    public void stopAudio(AudioSource source) {
        source.Stop();
    }

    public void enableGrab(bool e) { }

    public void enableUse(bool e) { }

    public void showGameObject(GameObject go, bool e) {
        if (e)
            go.transform.localScale = new Vector3(1, 1, 1);
        else
            go.transform.localScale = new Vector3(0, 0, 0);
    }

    public void showLight(Light light, bool e) {
        if (light.enabled != e)
            light.enabled = e;
    }


    /* Getters */
    public IState getIntroduction() {
        return INTRODUCTION;
    }

    public IState getTask1() {
        return TASK1;
    }

    public IState getTask2() {
        return TASK2;
    }

    public IState getComplete() {
        return COMPLETE;
    }

    /* Setters */
    public void setState(IState state) {
        currentState = state;
        currentState.Run();
    }
}