using System.Collections.Generic;

using Gengine.Core.Animation;

namespace Gengine.Core
{
	public class AnimationCollection
	{
		private readonly Dictionary<string, GameAnimation> Animations;
		private string ActiveAnimationName;

		public AnimationCollection()
		{
			Animations = new Dictionary<string, GameAnimation>();
			ActiveAnimationName = null;
		}

		public void Add(GameAnimation newAnimation) => Animations.Add(newAnimation.Name, newAnimation);
		public void Remove(string name) => Animations.Remove(name);
		public GameAnimation CurrentAnimation => Animations[ActiveAnimationName];
		public void SetAnimation(string name) => ActiveAnimationName = name;
	}
}