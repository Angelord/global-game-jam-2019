using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : Unit {

	private const int SCREEN_LOCK_MARGIN = 60;
	public static readonly Color[] PLAYER_COLORS = new Color[2] { new Color32(229, 69, 69, 255), new Color32(248, 115, 49, 255) };

	private static int lastIndex = 0;
	private static bool locked = false;

	[Header("Prefabs")]
	[SerializeField] private GameObject healthBar;
	[SerializeField] private GameObject speachBubblePref;
	[Header("Stats")]
	[SerializeField] private float dazeDuration;
	[SerializeField] private float regenRate;
	[SerializeField] private float mvmSpeed;
	[SerializeField] private GameObject dazeEffect;
	[Header("Arrow")]
	[SerializeField] private Sprite[] arrows;
	[SerializeField] private Transform[] arrowPositions;
	[SerializeField] private SpriteRenderer arrowSprite;
	[Header("Visuals")]
	[SerializeField] private RuntimeAnimatorController[] animControllers = new RuntimeAnimatorController[2];

	private int index;
	private Controls controls;
	private new Rigidbody rigidbody;
	private Direction direction = Direction.Down;
	private Counterbar bar;
	private SpeechBubble speech;
	private float lastRegen;
	private Usable[] usables;
	private int curUsable = 0;

	public static bool Locked { get { return locked; } set { locked = value; } }
	public Color Color { get { return PLAYER_COLORS[index]; } }
	public Direction Direction { get { return direction; } }

	public void Say(string text) {
		speech.Show(text);
		//TODO : play sound
	}

	protected override void OnTakeDamage(int amount) {
		bar.SetValue(Mathf.Clamp(CurHealth - amount, 0, int.MaxValue));
		lastRegen = Time.time;
	}

	protected override void Die() {
		dazeEffect.SetActive(true);
		EventManager.QueueEvent(new KidDiedEvent(this));
		Invoke("Undaze", dazeDuration);
	}

	private void Undaze() {	
		ResetHealth();
		bar.SetValue(CurHealth);
		dazeEffect.SetActive(false);
	}

	private void Start() {
		index = lastIndex++;

		usables = GetComponentsInChildren<Usable>();

		bar = Instantiate(healthBar, StageGUI.Instance.transform).GetComponent<Counterbar>();
		bar.SetValue(MaxHealth);
		bar.GetComponent<FollowerGUI>().SetTarget(transform);

		speech = Instantiate(speachBubblePref, StageGUI.Instance.transform).GetComponent<SpeechBubble>();
		speech.GetComponent<FollowerGUI>().SetTarget(transform);

		dazeEffect.SetActive(false);
		
		GetComponentInChildren<Animator>().runtimeAnimatorController = animControllers[index];

		arrowSprite.color = Color;

		controls.Horizontal = "Horizontal_" + index;
		controls.Vertical = "Vertical_" + index;
		controls.Attack = "Attack_" + index;

		rigidbody = GetComponent<Rigidbody>();

		EventManager.QueueEvent(new KidSpawnedEvent(this));
	}

	private void OnDestroy() {
		lastIndex--;
	}

	private void Update() {
		if(locked || Dead) { return; } 

		if(Input.GetButtonDown(controls.Attack)) {
			usables[curUsable].Use();
		}

		if(CurHealth < MaxHealth && (Time.time - lastRegen) >= regenRate) {
			CurHealth++;
			bar.SetValue(CurHealth);
			lastRegen = Time.time;
		}
	}

	private void FixedUpdate () {
		if(locked || Dead) { return; }

		float hor = Input.GetAxis(controls.Horizontal);
		float ver = Input.GetAxis(controls.Vertical);
		

		Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(SCREEN_LOCK_MARGIN, SCREEN_LOCK_MARGIN, 0));
		Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - SCREEN_LOCK_MARGIN, Screen.height - SCREEN_LOCK_MARGIN, 0.0f));

		if(transform.position.x <= topLeft.x && hor < 0.0f) {
			hor = 0.0f;
		}
		if(transform.position.x >= bottomLeft.x && hor > 0.0f) {
			hor = -0.0f;
		}
		if(transform.position.z <= topLeft.z && ver < 0.0f) {
			ver = 0.0f;
		}
		if(transform.position.z >= bottomLeft.z && ver > 0.0f) {
			ver = -0.0f;
		}

		Vector2 movDir = new Vector2(hor, ver);
		
		float magnitude = movDir.magnitude;
		if(magnitude > 0.0001f) {
			DetermineDir(hor, ver);
		}

		if(magnitude >= 1.0f) {
			movDir.Normalize();
		}

		rigidbody.AddForce(new Vector3(movDir.x, 0.0f, movDir.y) * mvmSpeed, ForceMode.Force);
	}

	private void DetermineDir(float hor, float ver) {

		if(Mathf.Abs(hor) > Mathf.Abs(ver)) {
			direction = hor > 0.0f ? Direction.Right : Direction.Left;
		}
		else {
			direction = ver > 0.0f ? Direction.Up : Direction.Down;
		}

		arrowSprite.transform.position = arrowPositions[(int)direction].position;
		arrowSprite.sprite = arrows[(int)direction];
		arrowSprite.flipX = direction == Direction.Left ? true : false;
	}

	private struct Controls {
		
		public string Horizontal { get; set; }
		public string Vertical { get; set; }
		public string Attack { get; set; }
	}
}
