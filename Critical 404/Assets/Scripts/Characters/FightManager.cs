using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class FightManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    public GameObject hitEffect;
    public GameObject blockEffect;

    private GameObject p1;
    private GameObject p2;
    private PlayerMovement p1script;
    private PlayerMovement p2script;
    private HealthBar[] healthbars;

    private bool[] registeringHit = {false, false};
    private GameObject turningPoint = null;

    private GameObject hitboxManager;

    private System.Random rng = new System.Random();

    public TMP_Text Whowon;

    void Awake()
    {
        hitboxManager = transform.Find("HitboxManager").gameObject;

        // Initialize local player references
        p1 = Instantiate(player1, new Vector3(-3f, 0f, 0f), Quaternion.identity);
        p2 = Instantiate(player2, new Vector3(3f, 0f, 0f), Quaternion.identity);
        p1script = p1.GetComponent<PlayerMovement>();
        p2script = p2.GetComponent<PlayerMovement>();
        p1script.SetFightManager(this.gameObject);
        p2script.SetFightManager(this.gameObject);
        p1script.playerId = 1;
        p2script.playerId = 2;

        // Initialize health bars
        healthbars = new HealthBar[] {
            GameObject.Find("Canvas/P1Healthbar").GetComponent<HealthBar>(),
            GameObject.Find("Canvas/P2Healthbar").GetComponent<HealthBar>()
        };
        healthbars[0].player = p1script;
        healthbars[1].player = p2script;
        healthbars[0].SetMaxHealth(p1script.hp);
        healthbars[1].SetMaxHealth(p2script.hp);

        // Initialize the "turning point" (point where characters flip around)
        turningPoint = transform.Find("TurningPoint").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float newPos = 0f;
        float p1x = p1.transform.position.x;
        float p2x = p2.transform.position.x;
        if (p1x > p2x)
        {
            newPos = ((p1x - p2x) / 2f) + p2x;
        }
        else
        {
            newPos = ((p2x - p1x) / 2f) + p1x;
        }
        p1script.SetTurningPoint(newPos);
        p2script.SetTurningPoint(newPos);
        turningPoint.transform.position = new Vector3(newPos, 0f, 0f);

        

    }

    /**
     *  Perform all necessary actions for when a player is hit. This function
     *  takes in as parameter the ID of the player who landed the attack, as
     *  well as the Hitbox that collided.
     */
    public void LandedHit(int attackedId, Hitbox hitbox)
    {
        // Blocker
        if (attackedId != 1 && attackedId != 2)
            throw new Exception(String.Format("Unknown interaction: Attacked player's ID set to '{0}'!", attackedId));

        int attackerId = attackedId == 1 ? 2 : 1;

        // Assign players from their scripts
        PlayerMovement attackingPlayer;
        PlayerMovement hitPlayer;
        if (attackerId == 1)
        {
            attackingPlayer = p1script;
            hitPlayer = p2script;
        }
        else
        {
            attackingPlayer = p2script;
            hitPlayer = p1script;
        }

        // Clear the attacking player's hitboxes (prevent double-hits)
        attackingPlayer.ClearHitboxesThisImage();

        // Generate position for particle effect
        System.Random rng = new System.Random();
        float rand = (float)(rng.NextDouble() * 0.5f) - 0.25f;
        bool hitPlayerFacingLeft = hitPlayer.GetComponent<SpriteRenderer>().flipX;
        Vector3 particlePos = new Vector3(   // put particle some distance near where the hitbox is
            attackingPlayer.transform.position.x +  // x pos
                (1.5f * (hitPlayerFacingLeft ? hitbox.offset.x : -hitbox.offset.x)) +
                NextSymmetricFloat(0.1f),
            attackingPlayer.transform.position.y +  // y pos
                (1.5f * hitbox.offset.y) +
                NextSymmetricFloat(0.1f),
            -1  // appear above characters
        );

        // Check if player is blocking
        if (hitPlayer.canBlock)
        {
            hitPlayer.blockstun = hitbox.blockstun; // apply blockstun from attack
            hitPlayer.SetVelocity(hitbox.knockback * (hitPlayerFacingLeft ? Vector2.right : Vector2.left));
            GameObject blockParticle = Instantiate(
                blockEffect, 
                particlePos,
                Quaternion.identity
            );
            blockParticle.GetComponent<SpriteRenderer>().flipX = hitPlayerFacingLeft;
            StartCoroutine(DoHitstop(3));
            return;
        }

        // Set hit player into hitstun and apply damage and other properties
        hitPlayer.hp -= hitbox.damage;
        healthbars[attackedId - 1].UpdateHealth();
        hitPlayer.hitstun = hitbox.hitstun;
        hitPlayer.SetVelocity(new Vector2(
            hitPlayerFacingLeft ? hitbox.knockback.x : -hitbox.knockback.x,
            hitbox.knockback.y
        ));
        hitPlayer.StopCurrentCoroutines();
        // Particle effects
        GameObject hitParticle = Instantiate(
            hitEffect, 
            particlePos,
            Quaternion.Euler(0, 0, NextSymmetricFloat(50))
        );
        hitParticle.GetComponent<SpriteRenderer>().flipX = hitPlayerFacingLeft;
        // Screenshake and hitstop effects
        // TODO: screenshake
        StartCoroutine(DoHitstop(3));
        Debug.Log("Player " + hitPlayer.playerId + " takes " + hitbox.damage + " damage!");

        // Check win condition
        if (hitPlayer.hp <= 0)
        {
            PlayerHasWon(attackerId);
        }        
    }

    public void PlayerHasWon(int winningPlayerId)
    {
        // TODO: Win condition stuff
        Debug.Log("Player " + winningPlayerId + " wins!");

        Whowon.text = ("Player ") + winningPlayerId + (" wins!");
        StartCoroutine(EndGame());
    }

    public IEnumerator EndGame()
    {
        p1script.canMove = false;
        p2script.canMove = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("PlayAgain");

    }

    public HitboxManager GetHitboxManager()
    {
        return hitboxManager.GetComponent<HitboxManager>();
    }

    /// Get a random float value between [-range, range]
    public float NextSymmetricFloat(float range)
    {
        return (float)(rng.NextDouble() * range) - (2 * range);
    }

    IEnumerator DoHitstop(float time)
    {
        float currTimescale = Time.timeScale;
        if (currTimescale == 0.0f) yield break;
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(time / 60f);
        Time.timeScale = currTimescale;
    }
}
