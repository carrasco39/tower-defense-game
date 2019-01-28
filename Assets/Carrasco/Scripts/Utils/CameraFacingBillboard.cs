using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Carrasco.Utils
{
    class CameraFacingBillboard : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);
        }
    }
}
