# Unity-Solo-Game-Project
__Goal:__ Create a game inspired/similar to Metroid : Zero Mission.

## Assets being used
[__Warped Caves__](https://assetstore.unity.com/packages/2d/characters/warped-caves-103250)

<img src ="https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/156e88b2-782e-46e9-baf5-fe2d573092b7" width="600" height="300">

<img src ="https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/fda702e6-cca9-4c07-8030-35f79fe19f04" width="600" height="300">

## Week 1
<details>
<summary>v0.0.1-alpha</summary>


* Created a really simple map, inspired by the first map of __Metroid:Zero Mission__.

![InitialMap](https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/1063a57b-e7f9-4504-8c20-b3e48bf515d8)

* Implemented Basic Movement for the character and the animations that go along with it.

![movement](https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/7e4d66dd-39f0-4019-994b-df434a91919a)

* Added enemies and basic shooting/enemy and enemy death logic and the animations that go along with them.

![shooting_and_enemy](https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/6bfe9485-9c9c-49cb-9b05-270d0fc1e40b)

__Things that need to fixed/added__
* need to remove impactEffect object that is instantiated when a bullet impacts anything (could cause lag later on in development)
* player damage logic
* enemy movement/logic
* parallax bg

</details>

## Week 2
<details>
<summary>v0.0.2-alpha</summary>

* Finished up the first map of the game, copied first map of Metroid/Metroid: Zero Mission

![FirstMap(Complete)](https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/9c6d344a-3fda-4125-87c0-326ea86bf8dc)

* Added animation for running and shooting, as well as interruptions to that animation to make movement smoother
* Also added a Parallax BG

https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/5adc960c-0240-4a04-8bad-df1035ce176b

![animator view](https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/59b64600-f9a1-4cb3-bf55-b7629e2cf297)

* Added Player health/take damage and its animations, and player death/player death animation

https://github.com/jkohh12/Unity-Solo-Game-Project/assets/136869443/f860429b-4aef-4bbb-9271-987e49d76a1e

__Smaller things added/fixed__
* deletion of all instantiated objects, causes less lag/less resources (impactEffect, deathEffect)
* made jumping system slightly smoother

__Things that need to fixed/added__
* player knockback
* enemy movement/logic
* fix player jump next to object (moon jump)
* wall jump?



</details>
