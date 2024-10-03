using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 0;
    public Vector3[] CheckPoint;
    
    private int _anchorMinDistance;
    private Vector3 _nextCheckpoint;
    private GameObject _player;
    void Start()
    {
        // Check nếu chưa gán Speed
        if(Speed <= 0)
            Speed = 4;
        _anchorMinDistance = 2;
        // Đưa player về vị trí xuất phát
        _player = GameObject.Find("Player");
        _player.transform.position = new Vector3(0,0,2);
        _nextCheckpoint = new Vector3(0, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        bool isTurn = false;
        CheckCurrentTaget(out isTurn);
        if (isTurn)
        {
            Debug.Log("Rotate");
            //xoay hướng oto theo target tiếp theo
            _player.transform.LookAt(_nextCheckpoint);
        }
        //Di chuyển đến target tiếp theo
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, _nextCheckpoint, Speed*Time.deltaTime);
    }

    /// <summary>
    /// Kiểm tra target hiện tại
    /// </summary>
    /// <param name="isTurn">rẽ hay không</param>
    private void CheckCurrentTaget(out bool isTurn)
    {
        isTurn = false;
        //Vị trí hiện tại của Player
        Vector3 mPosition = _player.transform.position;
        //tọa độ điểm kế tiếp
        for (int i = 0; i < CheckPoint.Length; i++) {
            //Khoảng cách đến điểm mốc kế tiếp
            float distanceToAnchor = Math.Abs((mPosition - CheckPoint[i]).magnitude);
            if (distanceToAnchor < _anchorMinDistance) { 
                if(i == CheckPoint.Length-1)
                    _nextCheckpoint = CheckPoint[0];
                else _nextCheckpoint = CheckPoint[i+1];
                isTurn = true;
                return;
            }
            else
            {
                isTurn = false;
            }
        }
    }
}
