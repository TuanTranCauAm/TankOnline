using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using FusionExamples.Tanknarok;

[CreateAssetMenu(fileName = "NewShootStyle", menuName = "ShootStyle")]
public class AbstractShootStyle : ScriptableObject
{
	public Activity activity;							
    public Transform[] _gunExits;                  				// Nơi đạn bay ra	
    public Projectile _projectilePrefab;           				// Networked projectile (cái này không hiểu ???)	
    public float _rateOfFire;     								// Thời gian nạp đạn	
    public byte _ammo;											// Lượng đạn
	public AudioEmitter _audioEmitter;							// Âm thanh
	public LaserSightLine _laserSight = null;					// Đường Lazer	
	public PowerupType _powerupType = PowerupType.DEFAULT;		// Loại bắn (để xác định hình dáng đạn và cách đạn nổ)	
	public ParticleSystem _muzzleFlashPrefab;					// Modun đẳng cấp đọc không thể hiểu, quá buồn
	//[Networked(OnChanged = nameof(OnFireTickChanged))]
	
	public void Constructor(){
		activity = new Activity();
		activity.Getup(_rateOfFire,_ammo,_powerupType,_laserSight,_audioEmitter,_gunExits,_projectilePrefab,_muzzleFlashPrefab);
	}
}
