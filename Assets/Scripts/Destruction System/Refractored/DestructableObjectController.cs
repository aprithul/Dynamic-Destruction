using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjectController : MonoBehaviour
{
    public GameObject root;
    [HideInInspector] public DestroyedPieceController root_dest_piece;

    private List<DestroyedPieceController> destroyed_pieces = new List<DestroyedPieceController>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var _dpc = child.gameObject.AddComponent<DestroyedPieceController>();
            var _rigidbody = child.gameObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = false;

            var _mc = child.gameObject.AddComponent<MeshCollider>();
            _mc.convex = true;
            destroyed_pieces.Add(_dpc);
        }
        root_dest_piece = root.GetComponent<DestroyedPieceController>();
        StartCoroutine(run_physics_steps(10));
    }

    private void Update()
    {
        
        if(DestroyedPieceController.is_dirty)
        {

            foreach (var destroyed_piece in destroyed_pieces)
            {
                destroyed_piece.visited = false;
            }


            // do a breadth first search to find all connected pieces
            find_all_connected_pieces(root_dest_piece);

            // drop all pieces not reachable from root
            foreach (var piece in destroyed_pieces)
            {
                if (piece && !piece.visited)
                {
                    piece.drop();
                }
            }
        }
    }

    private void find_all_connected_pieces(DestroyedPieceController destroyed_piece)
    {
        if (!destroyed_piece.visited)
        {
            if (!destroyed_piece.is_connected)
                return;
            destroyed_piece.visited = true;

            foreach (var _pdc in destroyed_piece.connected_to)
            {
                find_all_connected_pieces(_pdc);
            }
        }
        else
            return;
    }

    private IEnumerator run_physics_steps(int step_count)
    {
        for (int i = 0; i < step_count; i++)
            yield return new WaitForFixedUpdate();
        
        foreach( var piece in destroyed_pieces)
        {
            piece.make_static();
        }
    }
}
