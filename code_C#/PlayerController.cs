using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveForce = 3000f;
	public float maxMoveSpeed = 10.0f;
	public float jumpForce = 1000f;
	public float jumpSpeed = 20f;
	public int maxJumps = 3;
	public int player = 1;
	public int level = 1;
	public float maxControlTime = 5.0f;
	public float rotateSpeed = 50.0f;
	public float minEnemyControlDist = 2.0f;
	public Vector3 spawnPos;

	public GameObject otherPlayer;
	public GameObject gate;
	public GameObject otherGate;
	public GameObject puff;

	public Sprite Jump1;
	public Sprite Jump2;
	public Sprite Jump3;
	public Sprite Stand;
	public Sprite Move1;
	public Sprite Move2;
	public Sprite Move3;
	public Sprite Move4;
	public Sprite Move5;
	public Sprite StopFall;

	public Image j1;
	public Image j2;
	public Image j3;
	public Image c1;
	public Image c2;
	public Image c3;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private string HorizontalAxis = "Horizontal";
	private string JumpButton = "Jump";
	private string InteractButton = "Interact";
	private string Collectible;
	private string SafeEnemy;
	private string UnsafeEnemy;
	private string gateTrigger;
	private string checkpointTrigger;
	private int jumpNumber = 0;
	private float jumpTimer = 0.0f;
	private int numCollectibles = 0;
	//private bool doorAvailable = false;
	private float controlTimer;
	private float deathTimer;
	private GameObject controlledEnemy = null;
	private int maxCheckpointsChecked = 0;
	private bool soundPlaying = false;

	private int[] toCollect = {3, 3, 3};

	void Start () {
		spawnPos = gameObject.transform.position;
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		HorizontalAxis += player.ToString();
		JumpButton += player.ToString();
		InteractButton += player.ToString();
		if (player == 1) {
			Collectible = "LightCollectible";
			SafeEnemy = "LightEnemy";
			UnsafeEnemy = "DarkEnemy";
			gateTrigger = "WhiteGate";
			checkpointTrigger = "LightCheckpoint";
		} else if (player == 2) {
			Collectible = "DarkCollectible";
			SafeEnemy = "DarkEnemy";
			UnsafeEnemy = "LightEnemy";
			gateTrigger = "BlackGate";
			checkpointTrigger = "DarkCheckpoint";
		}
		controlTimer = maxControlTime;
		Physics2D.IgnoreCollision(otherPlayer.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}

	void Update () {

		if (jumpNumber == 0) {
			j1.color = new Color(j1.color.r, j1.color.g, j1.color.b, 1.0f);
			j2.color = new Color(j2.color.r, j2.color.g, j2.color.b, 1.0f);
			j3.color = new Color(j3.color.r, j3.color.g, j3.color.b, 1.0f);
		} else if (jumpNumber == 1) {
			j1.color = new Color(j1.color.r, j1.color.g, j1.color.b, 1.0f);
			j2.color = new Color(j2.color.r, j2.color.g, j2.color.b, 1.0f);
			j3.color = new Color(j3.color.r, j3.color.g, j3.color.b, 0.3f);
		} else if (jumpNumber == 2) {
			j1.color = new Color(j1.color.r, j1.color.g, j1.color.b, 1.0f);
			j2.color = new Color(j2.color.r, j2.color.g, j2.color.b, 0.3f);
			j3.color = new Color(j3.color.r, j3.color.g, j3.color.b, 0.3f);
		} else if (jumpNumber == 3) {
			j1.color = new Color(j1.color.r, j1.color.g, j1.color.b, 0.3f);
			j2.color = new Color(j2.color.r, j2.color.g, j2.color.b, 0.3f);
			j3.color = new Color(j3.color.r, j3.color.g, j3.color.b, 0.3f);
		}

		if (numCollectibles == 3) {
			c1.color = new Color(c1.color.r, c1.color.g, c1.color.b, 1.0f);
			c2.color = new Color(c2.color.r, c2.color.g, c2.color.b, 1.0f);
			c3.color = new Color(c3.color.r, c3.color.g, c3.color.b, 1.0f);
		} else if (numCollectibles == 2) {
			c1.color = new Color(c1.color.r, c1.color.g, c1.color.b, 1.0f);
			c2.color = new Color(c2.color.r, c2.color.g, c2.color.b, 1.0f);
			c3.color = new Color(c3.color.r, c3.color.g, c3.color.b, 0.3f);
		} else if (numCollectibles == 1) {
			c1.color = new Color(c1.color.r, c1.color.g, c1.color.b, 1.0f);
			c2.color = new Color(c2.color.r, c2.color.g, c2.color.b, 0.3f);
			c3.color = new Color(c3.color.r, c3.color.g, c3.color.b, 0.3f);
		} else if (numCollectibles == 0) {
			c1.color = new Color(c1.color.r, c1.color.g, c1.color.b, 0.3f);
			c2.color = new Color(c2.color.r, c2.color.g, c2.color.b, 0.3f);
			c3.color = new Color(c3.color.r, c3.color.g, c3.color.b, 0.3f);
		}

		if (deathTimer != 0.0f) {
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0.0f) {
				deathTimer = 0.0f;
				gameObject.transform.position = spawnPos;
			}
			return;
		}

		if (controlledEnemy == null) {

			if (rb.velocity.x > 0.1f) {
				sr.flipX = true;
			} else if (rb.velocity.x < -0.1f) {
				sr.flipX = false;
			}

			if (Mathf.Abs(rb.velocity.y) < 0.1f && rb.velocity.x > 0.1f) {
				if (rb.velocity.x < 0.5f) {
					sr.sprite = Stand;
				} else if (rb.velocity.x < 2.0f) {
					sr.sprite = Move1;
				} else if (rb.velocity.x < 4.0f) {
					sr.sprite = Move2;
				} else if (rb.velocity.x < 6.0f) {
					sr.sprite = Move3;
				} else if (rb.velocity.x < 8.0f) {
					sr.sprite = Move4;
				} else {
					sr.sprite = Move5;
				}
			} else if (Mathf.Abs(rb.velocity.y) < 0.1f && rb.velocity.x < -0.1f) {
				if (rb.velocity.x > -0.5f) {
					sr.sprite = Stand;
				} else if (rb.velocity.x > -2.0f) {
					sr.sprite = Move1;
				} else if (rb.velocity.x > -4.0f) {
					sr.sprite = Move2;
				} else if (rb.velocity.x > -6.0f) {
					sr.sprite = Move3;
				} else if (rb.velocity.x > -8.0f) {
					sr.sprite = Move4;
				} else {
					sr.sprite = Move5;
				}
			}

			if (jumpTimer > 0.0f) {
				jumpTimer -= Time.deltaTime;
				if (jumpTimer <= 0.12f) {
					sr.sprite = Jump2;
				} else if (jumpTimer <= 0.0f) {
					jumpTimer = 0.0f;
					sr.sprite = Jump3;
				}
			}

			if (jumpTimer == 0.0f) {
				if (rb.velocity.y > 0.12f) {
					sr.sprite = Jump3;
				} else if (rb.velocity.y < -2.0f) {
					sr.sprite = StopFall;
				}
			}

			float hor = Input.GetAxis(HorizontalAxis);
			bool up = Input.GetButtonDown(JumpButton);

			if (up) {
				jump();
			}

			if (hor > 0.0f && Mathf.Abs(rb.velocity.x) < maxMoveSpeed) {
				rb.AddForce(Vector2.right * moveForce * Time.deltaTime);
			} else if (hor < 0.0f && Mathf.Abs(rb.velocity.x) < maxMoveSpeed) {
				rb.AddForce(Vector2.left * moveForce * Time.deltaTime);
			}

			if (Input.GetButtonDown(InteractButton)) {
				findClosestEnemy();
			}

		} else {
			transform.position = controlledEnemy.transform.position;
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
			rb.velocity = new Vector2(0.0f, 0.0f);
			float hor = Input.GetAxis(HorizontalAxis);
			if (controlledEnemy.GetComponentInChildren<ShooterController>() != null) {
				controlledEnemy.GetComponentInChildren<ShooterController>().RotateControlledEnemy(-hor * Time.deltaTime * rotateSpeed);
			}
			controlTimer -= Time.deltaTime;
			if (controlTimer <= 3.0f && !soundPlaying) {
				soundPlaying = true;
				SoundManager.S.PlayHissingSound();
			}
			if (controlTimer <= 0.0f || Input.GetButtonDown(InteractButton)) {
				if (controlledEnemy.GetComponentInChildren<ShooterController>() != null) {
					controlledEnemy.GetComponentInChildren<ShooterController>().TriggerControl();
				}
				controlledEnemy = null;
				controlTimer = maxControlTime;
				soundPlaying = false;
			}
		}

	}

	void jump() {
		if (jumpNumber < maxJumps) {
			//rb.AddForce(Vector2.up * jumpForce);
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
			jumpTimer = 0.25f;
			sr.sprite = Jump1;
			jumpNumber++;
			SoundManager.S.PlayJumpSound();
		}
	}

	void findClosestEnemy() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(SafeEnemy);
		foreach (GameObject enemy in enemies) {
			if ((transform.position - enemy.transform.position).magnitude < (enemy.GetComponent<Renderer>().bounds.size.x/2 + minEnemyControlDist)) {
				controlledEnemy = enemy;
				if (controlledEnemy.GetComponentInChildren<ShooterController>() != null) {
					controlledEnemy.GetComponentInChildren<ShooterController>().TriggerControl();
					SoundManager.S.PlayTransformSound();
				}
				return;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (deathTimer != 0.0f) {
			return;
		}
		// Platform Collision
		if (col.gameObject.tag == "Platform") {
			Vector3 other_size = col.gameObject.GetComponent<Renderer> ().bounds.size;
			Vector3 self_size = gameObject.GetComponent<Renderer> ().bounds.size;
			if (col.gameObject.transform.position.y < transform.position.y &&
			    col.gameObject.transform.position.x - other_size.x / 2 - self_size.x / 3 < transform.position.x &&
			    col.gameObject.transform.position.x + other_size.x / 2 + self_size.x / 3 > transform.position.x) {
				jumpNumber = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (deathTimer != 0.0f) {
			return;
		}

		if (col.gameObject.tag == Collectible) {
			Destroy(col.gameObject);
			numCollectibles++;
			SoundManager.S.PlayCollectSound();
			if (numCollectibles == toCollect[level - 1]) {
				gate.GetComponent<GateController> ().ChangeGates (player);
				if (gate.GetComponent<GateController>().gateOpen || otherGate.GetComponent<GateController>().gateOpen) {
					gate.GetComponent<GateController> ().OpenGates ();
					otherGate.GetComponent<GateController> ().OpenGates ();
				}
				gate.GetComponent<GateController>().gateOpen = true;
				otherGate.GetComponent<GateController> ().gateOpen = true;
			}
		} else if (col.gameObject.tag == "OpenGate") {
			if (gate.GetComponent<GateController>().bothNext) {
				NextLevel ();
			} else if ((gate.GetComponent<GateController>().whiteNext && player != 1) || (gate.GetComponent<GateController>().blackNext && player != 2)) {
				otherGate.GetComponent<GateController>().bothNext = true;
				gate.GetComponent<GateController>().bothNext = true;
			} else if (gateTrigger == "WhiteGate") {
				gate.GetComponent<GateController>().whiteNext = true;
				otherGate.GetComponent<GateController>().whiteNext = true;
			} else if (gateTrigger == "BlackGate") {
				gate.GetComponent<GateController>().blackNext = true;
				otherGate.GetComponent<GateController>().blackNext = true;
			}
		} else if (col.gameObject.tag == checkpointTrigger) {
			if (level == 1) {
				if (col.gameObject.name == checkpointTrigger + " (1)") {
					if (maxCheckpointsChecked < 1 && numCollectibles >= 1) {
						Vector3 pos = col.gameObject.GetComponent<CheckpointController>().PlayerTouch();
						if (pos.x != -3000.0f) {
							spawnPos = pos;
							otherPlayer.GetComponent<PlayerController>().spawnPos = pos;
						}
						maxCheckpointsChecked = 1;
						SoundManager.S.PlayCollectSound();
					}
				} else if (col.gameObject.name == checkpointTrigger + " (2)") {
					if (maxCheckpointsChecked < 2 && numCollectibles >= 1) {
						Vector3 pos = col.gameObject.GetComponent<CheckpointController>().PlayerTouch();
						if (pos.x != -3000.0f) {
							spawnPos = pos;
							otherPlayer.GetComponent<PlayerController>().spawnPos = pos;
						}
						maxCheckpointsChecked = 2;
						SoundManager.S.PlayCollectSound();
					}
				} else if (col.gameObject.name == checkpointTrigger + " (3)") {
					if (maxCheckpointsChecked < 3 && numCollectibles >= 2) {
						Vector3 pos = col.gameObject.GetComponent<CheckpointController>().PlayerTouch();
						if (pos.x != -3000.0f) {
							spawnPos = pos;
							otherPlayer.GetComponent<PlayerController>().spawnPos = pos;
						}
						maxCheckpointsChecked = 3;
						SoundManager.S.PlayCollectSound();
					}
				}
			} else if (level == 2) {
				if (col.gameObject.name == checkpointTrigger + " (1)") {
					if (maxCheckpointsChecked < 1 && numCollectibles >= 1) {
						Vector3 pos = col.gameObject.GetComponent<CheckpointController>().PlayerTouch();
						if (pos.x != -3000.0f) {
							spawnPos = pos;
							otherPlayer.GetComponent<PlayerController>().spawnPos = pos;
						}
						maxCheckpointsChecked = 1;
						SoundManager.S.PlayCollectSound();
					}
				} else if (col.gameObject.name == checkpointTrigger + " (2)") {
					if (maxCheckpointsChecked < 2 && numCollectibles >= 2) {
						Vector3 pos = col.gameObject.GetComponent<CheckpointController>().PlayerTouch();
						if (pos.x != -3000.0f) {
							spawnPos = pos;
							otherPlayer.GetComponent<PlayerController>().spawnPos = pos;
						}
						maxCheckpointsChecked = 2;
						SoundManager.S.PlayCollectSound();
					}
				}
			}
		} else if (col.gameObject.tag == SafeEnemy) {
			Vector3 pos1 = col.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
			Vector3 pos2 = col.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
			Vector3 pos3 = col.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);

			Vector3 vel1 = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);
			Vector3 vel2 = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);
			Vector3 vel3 = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);

			float scale1 = Random.Range(0.05f, 0.1f);
			float scale2 = Random.Range(0.05f, 0.1f);
			float scale3 = Random.Range(0.05f, 0.1f);

			GameObject puff1 = (GameObject)Instantiate(puff, pos1, Quaternion.identity);
			GameObject puff2 = (GameObject)Instantiate(puff, pos2, Quaternion.identity);
			GameObject puff3 = (GameObject)Instantiate(puff, pos3, Quaternion.identity);

			puff1.GetComponent<PuffController>().velocity = vel1;
			puff2.GetComponent<PuffController>().velocity = vel2;
			puff3.GetComponent<PuffController>().velocity = vel3;

			puff1.transform.localScale = new Vector3(scale1, scale1, 1.0f);
			puff2.transform.localScale = new Vector3(scale2, scale2, 1.0f);
			puff3.transform.localScale = new Vector3(scale3, scale3, 1.0f);
			jumpNumber = 0;
		} else if (col.gameObject.tag == UnsafeEnemy) {
			Vector3 pos1 = col.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
			Vector3 pos2 = col.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
			Vector3 pos3 = col.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);

			Vector3 vel1 = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);
			Vector3 vel2 = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);
			Vector3 vel3 = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);

			float scale1 = Random.Range(0.05f, 0.1f);
			float scale2 = Random.Range(0.05f, 0.1f);
			float scale3 = Random.Range(0.05f, 0.1f);

			GameObject puff1 = (GameObject)Instantiate(puff, pos1, Quaternion.identity);
			GameObject puff2 = (GameObject)Instantiate(puff, pos2, Quaternion.identity);
			GameObject puff3 = (GameObject)Instantiate(puff, pos3, Quaternion.identity);

			puff1.GetComponent<PuffController>().velocity = vel1;
			puff2.GetComponent<PuffController>().velocity = vel2;
			puff3.GetComponent<PuffController>().velocity = vel3;

			puff1.transform.localScale = new Vector3(scale1, scale1, 1.0f);
			puff2.transform.localScale = new Vector3(scale2, scale2, 1.0f);
			puff3.transform.localScale = new Vector3(scale3, scale3, 1.0f);
			GetHit ();
		}
	}

	public void NextLevel() {
		SceneManager.LoadScene(level + 3);
	}

	public void GetHit() {
		if (deathTimer == 0.0f) {
			deathTimer = 2.0f;
			SoundManager.S.PlayDeathSound();
		}
	}
}