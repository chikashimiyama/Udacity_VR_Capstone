Goal
====

-   Musical instrument for non musicians in VR

 

Initial ideas from brainstorming
================================

 

-   Musical instrument for non musicians

-   Use Oculus Touch as a musical controller

-   Sampled sound from microphone

-   3D musical score

-   Synthesizer Control in 3D

-   Integrated Sound Engine in Unity

-   Realtime Sound Visualization

-   Unique 3D Control surface

-   Intuitive UI

-   Very flexible sound control

-   Record of performance

-   Audience can watch the performance of the instrument via network

 

Capstone Scoping
================

 

-   Musical instrument for non musicians

-   Use Oculus Touch as a musical controller

    -   Revised: Using the new SteamVR

-   Sampled sound from microphone

    -   Rejected: Recording process introduces more complexity for the
        development and the user

-   3D musical score

    -   Rejected: This also introduces a steep learning curve. The user have to
        learn how to read / write score

-   Synthesizer Control in 3D

    -   Revised: It is possible to let user control some audio parameters by
        putting some reference lines in the space

-   Integrated Sound Engine in Unity

    -   Revised: using this library https://github.com/Magicolo/uPD

-   Realtime Sound Visualization

    -   Revised: both time-domain / frequency-domain data can be obtained from
        uPD library. With this the visualization of the audio signal and
        spectrum are relatively easy

    -   Revised: Chronological representation spectral transition, using
        geometry shader

    -   Revised: Realtime Calculation of spectral centroid, using compute shader

-   Intuitive UI

    -   Revised: 3D button designed with blender

-   Very flexible sound control

    -   Revised: we can use yaw, pitch, roll parameter of the controller

    -   Revised: we can assign each hand different functionality

-   Record of performance

    -   Abandoned: It’s possible to do that with another software

-   Audience can watch the performance of the instrument via network

    -   Abandoned: the scope becomes to big
