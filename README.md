# Tween Jobs
(WIP) Tween engine for Unity based on the C# Job System.


## Features
- Supports tweening `float`, `Vector2`, `Vector3`, `Vector4`, `Quaternion`, `Color` and `Rect` values
- Highly configurable: easing function, duration, animation speed, `deltaTime` vs `unscaledDeltaTime`, repeat vs ping-pong loops
- Tweeners are serializable C# classes that can be easily embedded in your own components
- Ready-made tween components with embedded tweeners for ease of integration
- Tween math runs in parallel jobs using the C# Job System.
  Jobs are compiled with Burst for maximum performance


## Dependencies
- [Update Manager](https://github.com/gilzoide/unity-update-manager): Update Manager is used to manage tween jobs
- [Burst](https://docs.unity3d.com/Packages/com.unity.burst@1.8/manual/index.html): used to compile jobs
- [Unity Mathematics](https://docs.unity3d.com/Packages/com.unity.mathematics@1.3/manual/index.html): math library


## How to install
Either:
- Install using the [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html) with the following URL:
  ```
  https://github.com/gilzoide/unity-tween-jobs.git#1.0.0-preview1
  ```
- Clone this repository or download a snapshot of it directly inside your project's `Assets` or `Packages` folder.


## Creating your own tweener
```cs
// MySpriteColorTween.cs
using System;
using Gilzoide.TweenJobs;

// 1. Create a serializable class that inherits A*Tweener
// Supported tween types: float, Vector2, Vector3, Vector4, Quaternion, Color and Rect
[Serializable]
public class MySpriteColorTweener : AColorTweener
{
    public SpriteRenderer targetSprite;

    // 2. Implement the `Value` property
    public override Color Value
    {
        get => targetSprite.color;
        set => targetSprite.color = value;
    }
}

// 3. (optional) Create a wrapper component for your tweener
public class MySpriteColorTween : ATweenComponent<MySpriteColorTweener>
{
}

// 4. Use your tweener/tween component just like the builtin ones.
// Enjoy 🍾
```
