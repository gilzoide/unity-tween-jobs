# Tween Jobs
Tween engine for Unity based on the C# Job System.


## Features
- Supports tweening `float`, `Vector2`, `Vector3`, `Vector4`, `Quaternion` and `Color` values
- Highly configurable: easing function, duration, animation speed, `deltaTime` vs `unscaledDeltaTime`, repeat vs ping pong loops
- Tweeners are serializable and can be easily embedded in your own components
- Jobs are compiled using Burst for maximum performance
- Tweeners are pure C# classes and don't require `MonoBehaviours`.
  There are `MonoBehaviour`s with embedded tweeners for ease of integration.


## Dependencies
- [Update Manager](https://github.com/gilzoide/unity-update-manager): Update Manager is used to manage tween jobs
- [Burst](https://docs.unity3d.com/Packages/com.unity.burst@1.8/manual/index.html): used to compile jobs