using System.Collections.Generic;
using Fusion;
using FusionExamples.Utility;
using UnityEngine;
using FusionExamples.Tanknarok;
using System;

public class Activity : NetworkBehaviour{
	public int fireTick { get; set; }
	public int _gunExit;															// Nơi đạn bay ra
	public float _visible;	
	public bool _active;															// Cờ cho lazer
	public List<ParticleSystem> _muzzleFlashList = new List<ParticleSystem>();		// Hiệu ứng nồng súng khi bắn đạn
	public float delay;												                // Thời gian nạp đạn
	public bool isShowing => _visible > 1.0f;										// Hiển thị đạn
	public byte ammo;                                                               // Lượng đạn
	public PowerupType powerupType;
    public LaserSightLine _laserSight;
    public AudioEmitter _audioEmitter;
    public Transform[] _gunExits;      
    public Projectile _projectilePrefab;             									

	public void Getup(float _rateOfFire,byte _ammo,PowerupType _powerupType,LaserSightLine _laserSight,AudioEmitter _audioEmitter,Transform[] _gunExits,Projectile _projectilePrefab,ParticleSystem _muzzleFlashPrefab){                               // Khởi tạo hiệu ứng bắn
        this.delay = _rateOfFire;
        this.ammo = _ammo;
        this.powerupType = _powerupType;
        this._laserSight = _laserSight;
        this._audioEmitter = _audioEmitter;
        this._gunExits = _gunExits;
        this._projectilePrefab = _projectilePrefab;
		if (_muzzleFlashPrefab != null){
			//foreach (Transform gunExit in _gunExits){
				//_muzzleFlashList.Add(Instantiate(_muzzleFlashPrefab, gunExit.position, gunExit.rotation, transform));
			//}
		}
	}

    internal void Getup(float rateOfFire, byte ammo, PowerupType powerupType, LaserSightLine laserSight, AudioEmitter audioEmitter, Projectile projectilePrefab, ParticleSystem muzzleFlashPrefab)
    {
        throw new NotImplementedException();
    }

    public void Show(bool show){								                    // Hiển thị nồng súng
		if (_active && !show)
            ToggleActive(false);
        else if (!_active && show)
			ToggleActive(true);
		_visible = Mathf.Clamp(_visible + (show ? Time.deltaTime : -Time.deltaTime) * 5f, 0, 1);
        /*if (show)
			transform.localScale = Tween.easeOutElastic(0, 1, _visible) * Vector3.one;
		else
			transform.localScale = Tween.easeInExpo(0, 1, _visible) * Vector3.one;*/
    }

	public void ToggleActive(bool value){                                          // Hiển thị lazer
		_active = value;
        if (_laserSight != null){
			if (_active){
				_laserSight.SetDuration(0.5f);
				_laserSight.Activate();
			}else
				_laserSight.Deactivate();
		}
	}
    
	public void Fire(NetworkRunner runner, PlayerRef owner, Vector3 ownerVelocity, Vector3 position, Quaternion rotation){ // Hiển thị ra viên đạn và đạn bay
		Debug.Log("bắn.............");
		//if (powerupType == PowerupType.EMPTY || _gunExits.Length == 0)
		//	return;
			
		//Transform exit = GetExitPoint();
		//Debug.Log(exit);
		SpawnNetworkShot(runner, owner, /*exit,*/ ownerVelocity, position, rotation);
		//fireTick = Runner.Simulation.Tick;
	}

	public void OnFireTickChanged(Changed<Activity> changed){                // Hiển thị cục nổ trước nồng súng
		changed.Behaviour.FireFx();
	}

	public void FireFx(){                                                          // Hiển thị cục nổ trước nồng súng
		if (_laserSight != null)
			_laserSight.Recharge();
		if(_gunExit<_muzzleFlashList.Count)
			_muzzleFlashList[_gunExit].Play();
		_audioEmitter.PlayOneShot();
	}
    
	public void SpawnNetworkShot(NetworkRunner runner, PlayerRef owner, /*Transform exit,*/ Vector3 ownerVelocity, Vector3 position, Quaternion rotation){
		//Debug.Log($"Băn viên đạn tại tick: {Runner.Simulation.Tick} \nstage={Runner.Simulation.Stage}");
		Debug.Log("Bắn đê............");
		var key = new NetworkObjectPredictionKey {Byte0 = (byte) owner.RawEncoded, Byte1 = (byte) runner.Simulation.Tick};
		runner.Spawn(
				_projectilePrefab, 
				/*exit.position,*/position, 
				/*exit.rotation,*/rotation,
				owner, 
				(runner, obj) =>{ obj.GetComponent<Projectile>().InitNetworkState(ownerVelocity);}, 	// Bắn ??? Không hiểu ???
				key );
	}

	public Transform GetExitPoint(){                                               // Vị trí đạn xuất hiện
		_gunExit = (_gunExit + 1) % _gunExits.Length;
		Transform exit = _gunExits[_gunExit];
		return exit;
	}
    
}