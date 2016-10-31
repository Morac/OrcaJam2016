using UnityEngine;
using DG.Tweening;

public class FancyPlayerMover : MonoBehaviour
{
	public enum State
	{
		Descending,
		Ascending
	}

	State CurrentState = State.Descending;

	public float DescendSpeed = -2;
	public float AscendSpeed = 2;

	public float SlowdownAmount = 0.5f;

	public float LateralSpeed = 4;

	public FancyPlayerActions actions;
	public Target caughtTarget { get; private set; }

	public ParticleSystem victoryParticles;

	void Awake()
	{
		actions = FancyPlayerActions.Create();
	}

	void Update()
	{
		Vector3 movement = new Vector3();
		if (CurrentState == State.Ascending)
			movement.y = AscendSpeed;
		else
			movement.y = DescendSpeed;

        if (transform.position.y > 0)
            movement.y *= 2;

		movement.x = actions.MoveLateral.Value * LateralSpeed;

		//slowdown for finer control
		if (actions.StartGame.IsPressed)
			movement *= SlowdownAmount;

		transform.position += movement * Time.deltaTime;
	}

	public void AttachTo(Target target)
	{
		if (caughtTarget != null)
			return;

		caughtTarget = target;

		//disable any tweens on target
		target.OnCapture();

		//add joint
		caughtTarget.transform.parent = transform;

		CurrentState = State.Ascending;
	}

	public void HitObstacle(Obstacle obstacle)
	{
		enabled = false;

		UIManager.Instance.DoFade();

		var seq = DOTween.Sequence();

		GameManager.Instance.Music.DOFade(0, 1).SetUpdate(true);
		GameManager.Instance.Ambience.DOFade(0, 1).SetUpdate(true);
		seq.Append(DOTween.To(() => Time.timeScale, v => Time.timeScale = v, 0, 1).SetUpdate(true));
		seq.AppendInterval(2).SetUpdate(true);
		seq.OnComplete(() =>
		{
			Time.timeScale = 1;
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		});
	}
}
