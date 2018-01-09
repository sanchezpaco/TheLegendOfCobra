using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

public class AnimatorController : MonoBehaviour
{

    private IDictionary<string, string> animatorVariables;
    private Animator animator;
    private string me;
    private Type type;
    private MethodInfo methodInfo;

    // Use this for initialization
    void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.animatorVariables = new Dictionary<string, string>();
        this.me = this.tag;
        this.type = this.GetType();
        this.methodInfo = type.GetMethod("Create" + me + "Variables", (BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic));
        if (methodInfo != null)
        {
            this.methodInfo.Invoke(this, new object[] { });
        }

    }

    public void Animate(string state)
    {
		this.methodInfo = type.GetMethod("Animate" + me, (BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic));
        if (methodInfo != null)
        {
            this.methodInfo.Invoke(this, new object[] { state });
        }
    }

    void AnimatePlayer(string state)
    {
        try { 
            string currentState = this.animatorVariables[state];
            this.animator.SetTrigger(currentState);

        } catch(Exception ex) {}

    }

	void AnimateEnemy(string state)
	{
		try { 
			string currentState = this.animatorVariables[state];
			this.animator.SetTrigger(currentState);
		} catch(Exception ex) {}

	}

	void AnimateSpawnEffects(string state)
	{
		try { 
			string currentState = this.animatorVariables[state];
			this.animator.SetTrigger(currentState);
		} catch(Exception ex) {}

	}

	void CreateEnemyVariables()
	{
		this.animatorVariables[""] = "Walk";
		this.animatorVariables["Walk"] = "Walk";
		this.animatorVariables ["Attack"] = "Attack";
		this.animatorVariables ["SpecialAttack"] = "SpecialAttack";
		this.animatorVariables["Die"] = "Dead";
	}

	void CreateSpawnEffectsVariables() {
		this.animatorVariables["Spawn"] = "Spawn";
	}

    void CreatePlayerVariables()
    {


        this.animatorVariables[""] = "IdleUp";
        this.animatorVariables["up"] = "Up";
        this.animatorVariables["upleft"] = "Left";
		this.animatorVariables["upright"] = "Right";

		this.animatorVariables["down"] = "Down";
		this.animatorVariables["downleft"] = "Left";
		this.animatorVariables["downright"] = "Right";

        this.animatorVariables["left"] = "Left";
        this.animatorVariables["right"] = "Right";

		this.animatorVariables["stopped"] = "IdleUp";
		this.animatorVariables["stoppedup"] = "IdleUp";
		this.animatorVariables["stoppedright"] = "IdleRight";
		this.animatorVariables["stoppedleft"] = "IdleLeft";
		this.animatorVariables["stoppeddown"] = "IdleDown";


		this.animatorVariables["attack"] = "AttackUP";
		this.animatorVariables["attackup"] = "AttackUP";
		this.animatorVariables["attackupleft"] = "AttackLeft";
		this.animatorVariables["attackupright"] = "AttackRight";

		this.animatorVariables["attackdown"] = "AttackDown";
		this.animatorVariables["attackdownleft"] = "AttackLeft";
		this.animatorVariables ["attackdownright"] = "AttackRight";

		this.animatorVariables["attackleft"] = "AttackLeft";
		this.animatorVariables["attackright"] = "AttackRight";

		this.animatorVariables["attackstopped"] = "AttackUP";
		this.animatorVariables["attackstoppedup"] = "AttackUP";
		this.animatorVariables["attackstoppedright"] = "AttackRight";
		this.animatorVariables["attackstoppedleft"] = "AttackLeft";
		this.animatorVariables["attackstoppedupright"] = "AttackRight";
		this.animatorVariables["attackstoppedupleft"] = "AttackLeft";
		this.animatorVariables["attackstoppeddownright"] = "AttackRight";
		this.animatorVariables["attackstoppeddownleft"] = "AttackLeft";
		this.animatorVariables["attackstoppeddown"] = "AttackDown";


		this.animatorVariables["rolling"] = "RollUp";
		this.animatorVariables["rollingup"] = "RollUp";
		this.animatorVariables["rollingupleft"] = "RollLeft";
		this.animatorVariables["rollingupright"] = "RollRight";

		this.animatorVariables["rollingdown"] = "RollDown";
		this.animatorVariables["rollingdownleft"] = "RollLeft";
		this.animatorVariables ["rollingdownright"] = "RollRight";

		this.animatorVariables["rollingleft"] = "RollLeft";
		this.animatorVariables["rollingright"] = "RollRight";

		this.animatorVariables["rollingstopped"] = "RollUp";
		this.animatorVariables["rollingstoppedup"] = "RollUp";
		this.animatorVariables["rollingstoppedright"] = "RollRight";
		this.animatorVariables["rollingstoppedleft"] = "RollLeft";
		this.animatorVariables["rollingstoppedupright"] = "RollRight";
		this.animatorVariables["rollingstoppedupleft"] = "RollLeft";
		this.animatorVariables["rollingstoppeddownright"] = "RollRight";
		this.animatorVariables["rollingstoppeddownleft"] = "RollLeft";
		this.animatorVariables["rollingstoppeddown"] = "RollDown";

    }
    
}
