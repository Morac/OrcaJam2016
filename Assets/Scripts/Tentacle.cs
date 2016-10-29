using UnityEngine;
using DG.Tweening;

public class Tentacle : MonoBehaviour
{
	public float RotateVarianceMin = 10;
	public float RotateVarianceMax = 40;
	public float RotateSpeedMin = 1;
	public float RotateSpeedMax = 3;

	public float ScaleSpeedMin = 2;
	public float ScaleSpeedMax = 5;

	void Start()
	{
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

		var angleVar = Random.Range(RotateVarianceMin, RotateVarianceMax);
		var speed = Random.Range(RotateSpeedMin, RotateSpeedMax);

		transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, angleVar), speed).SetLoops(-1, LoopType.Yoyo);

		transform.DOScaleX(transform.localScale.x * 2, Random.Range(ScaleSpeedMin, ScaleSpeedMax)).SetLoops(-1, LoopType.Yoyo);
	}
}
