using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockBackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;
    public AnimationCurve knockBackForceCurve;

    private Rigidbody2D rb;

    private Coroutine knockBackCoroutine;

    public bool isBeingKnockedBack { get; private set; }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public IEnumerator knockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {

        isBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;
        float _time = 0f;

  
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;

        while(_elapsedTime < knockBackTime)
        {
            //iterate timer
            _elapsedTime += Time.fixedDeltaTime;
            _time += Time.fixedDeltaTime;

            //update hitForce
            _hitForce = hitDirection * hitDirectionForce * knockBackForceCurve.Evaluate(_time);

            //combine _hitForce and _constantForce
            _knockbackForce = _hitForce + _constantForce;

            //combine knockBackForce with Input Force
            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector2(inputDirection * inputForce, 0f);
            }
            else
            {
                _combinedForce = _knockbackForce;
            }

            //apply knockback
            rb.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();

        }

        isBeingKnockedBack = false;
    }

    public void callKnockback(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockBackCoroutine = StartCoroutine(knockBackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
