using UnityEngine;

public class WaitForAnimation : CustomYieldInstruction {

    private Animator animator_;
    private int last_state_hash_ = 0;
    private int layer_no_ = 0;

    public override bool keepWaiting {
        get {
            var currentAnimatorState = animator_.GetCurrentAnimatorStateInfo(layer_no_);
            return currentAnimatorState.fullPathHash == last_state_hash_ && (currentAnimatorState.normalizedTime < 1);
        }
    }

    public WaitForAnimation(Animator animator, int layer_no) {
        Init(animator, layer_no, animator.GetCurrentAnimatorStateInfo(layer_no).fullPathHash);
    }

    private void Init(Animator animator, int layer_no, int hash) {
        animator_ = animator;
        layer_no_ = layer_no;
        last_state_hash_ = hash;
    }

}
