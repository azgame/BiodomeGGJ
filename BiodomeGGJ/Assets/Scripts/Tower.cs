using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Tower : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    protected int  maxAmmo;
    protected int  currentAmmo;
    public Vector3 colorRGB;
    [SerializeField]
    protected float attackTimeMax;
    [SerializeField]
    protected float attackTimeCurrent;
    public Slider ammoslider;
    public Image ammoFillColour;
    int flashCounter;
    int flashTimer;
    protected int damage;
    public GameObject bullet;
    public GameObject spawnLocation;
    public List<GameObject> myEnemies;
    //float ammo;

    bool isInPickupRange = false;
    bool isInRepairOrReloadRange = false;
    bool isCarried = true;

    // UI Elements
    List<Image> brokenUI = new List<Image>();
    List<Image> repairedUI = new List<Image>();

    virtual protected void Start()
    {
        
        myEnemies=new List<GameObject>();
        myEnemies.Clear();
        if(ammoslider)
        {
            ammoslider.maxValue = maxAmmo;
            ammoslider.value = currentAmmo;
        }

        flashTimer = 20;
        flashCounter = 0;
    }

    virtual protected void initialize()
    {
        foreach(Transform child in this.transform) {
            if (child.tag == "BrokenTower") {
                foreach(Transform gChild in child) {
                    if (gChild.tag == "TowerRepairUI") {
                        foreach(Transform ggChild in gChild) {
                            brokenUI.Add(ggChild.GetComponent<Image>());
                        }
                    }
                }
            }
        }
        foreach(Transform child in this.transform) {
            if (child.tag == "RepairedTower") {
                foreach(Transform gChild in child) {
                    if (gChild.tag == "AmmoUI") {
                        foreach(Transform ggChild in gChild) {
                            if (ggChild.tag == "TowerRepairUI") {
                                foreach(Transform g3Child in ggChild) {
                                    repairedUI.Add(g3Child.GetComponent<Image>());
                                }
                            }
                        }
                    }
                }
            }
        }
        this.makeBroken();
    }
    
    virtual protected void Update()
    {
        
        if (myEnemies.Count != 0)
        {
        this.gameObject.transform.LookAt(myEnemies[0].transform);
        transform.LookAt(new Vector3(myEnemies[0].transform.position.x, transform.position.y, myEnemies[0].transform.position.z));
        attack();
        }

        if (currentAmmo <= 0 && this.fillCount() > 2)
        {
            flashCounter++;
            if (flashCounter % flashTimer == 0 || flashCounter % flashTimer == 1)
                ammoFillColour.color = Color.red;
            else
                ammoFillColour.color = Color.white;

            if (flashCounter % 50 == 0)
            {
                flashTimer--;
            }

            if (flashCounter > 900)
                makeBroken();
        }
        else
        {
            flashTimer = 20;
            flashCounter = 0;
        }


        
         if (this.fillCount() >= 3 && myEnemies.Count != 0)
         {
            this.gameObject.transform.LookAt(myEnemies[0].transform);
            transform.LookAt(new Vector3(myEnemies[0].transform.position.x, transform.position.y, myEnemies[0].transform.position.z));
            attack();
         }
    }

    virtual  protected void attack()
    {
        if (this.fillCount() >= 3 && attackTimeCurrent <= 0 && currentAmmo > 0 )
        {
            Instantiate(bullet,spawnLocation.transform.position,spawnLocation.transform.rotation);
            bullet.GetComponent<Bullet>().colorRGB.Set(colorRGB.x,colorRGB.y,colorRGB.z);
            bullet.GetComponent<Bullet>().damage = damage;
            currentAmmo--;
            ammoslider.value = currentAmmo;

            attackTimeCurrent = attackTimeMax;
        }
         if (attackTimeCurrent>=0)
        {

            attackTimeCurrent--;
        }

        
    }

    virtual  protected void reload()
    {
        this.currentAmmo = this.maxAmmo;
        ammoslider.value = currentAmmo;
    }

    public void EnemyDeath(GameObject deadenemy)
    {
        if(myEnemies.Contains(deadenemy))
        {
            myEnemies.Remove(deadenemy);
        }
    }

    virtual protected void onBreak()
    {
        foreach(Transform child in this.transform)
        {
            if (child.tag == "RepairedTower") {
                child.gameObject.SetActive(false);
            } else if (child.tag == "BrokenTower") {
                child.gameObject.SetActive(true);
            }
            foreach(Image i in this.brokenUI) {
                i.color = new Color(200, 200, 200);
            }
        }
    }

    virtual protected void onRepairComplete()
    {
        foreach(Transform child in this.transform)
        {
            if (child.tag == "RepairedTower") {
                child.gameObject.SetActive(true);
            } else if (child.tag == "BrokenTower") {
                child.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < 3; i++) {
            repairedUI[i].color = brokenUI[i].color;
        }
        this.reload();
    }

    virtual protected void onRepair(InventoryItem resourceType)
    {
        Color newColor = new Color();
        switch (resourceType) {
            case InventoryItem.RED:
                {
                    this.colorRGB.x += 1;
                    newColor = new Color(255, 0, 0, 100);
                    break;
                }
            case InventoryItem.GREEN:
                {
                    this.colorRGB.y += 1;
                    newColor = new Color(0, 255, 0, 100);
                    break;
                }
            case InventoryItem.BLUE:
                {
                    this.colorRGB.z += 1;
                    newColor = new Color(0, 0, 255, 100);
                    break;
                }
        }
        this.brokenUI[(int)this.fillCount() - 1].color = newColor;
        if (this.fillCount() == 3) {
            this.onRepairComplete();
        }
    }

    virtual protected void makeBroken()
    {
        this.colorRGB.Set(0, 0, 0);
        this.flashTimer = 20;
        this.flashCounter = 0;
        this.onBreak();
    }
    public float fillCount()
    {
        return colorRGB.x + colorRGB.y + colorRGB.z;
    }

    public bool hasBlockType(InventoryItem resourceType) {
        switch (resourceType) {
            case InventoryItem.RED: 
                return this.colorRGB.x > 0;
            case InventoryItem.GREEN:
                return this.colorRGB.y > 0;
            case InventoryItem.BLUE:
                return this.colorRGB.z > 0;
            default:
                return false;
        }
    }
    public bool wasTriggered(IInteractable inventory)
    {
        if (inventory == null) {
            this.isInPickupRange = true;
            return true;
        } else if (inventory.getInventoryType() != InventoryItem.TOWER) {
            if (this.fillCount() < 3 || this.hasBlockType(inventory.getInventoryType())) {
                this.isInRepairOrReloadRange = true;
                return true;
            }
        }
        return false;
    }

    public void wasUntriggered()
    {
        this.isInPickupRange = false;
    }

    public IInteractable activated(IInteractable inventory)
    {
        if (inventory == null) {
            this.isCarried = true;
            this.makeBroken();
            return this;
        } else if (inventory.getInventoryType() != InventoryItem.TOWER) {
            if (this.fillCount() < 3) {
                this.onRepair(inventory.getInventoryType());
            } else {
                this.reload();
            }
            inventory.consumed();
        }
        return null;
    }

    public bool deactivated()
    {
        this.isCarried = false;
        return false;
    }

    public InventoryItem getInventoryType()
    {
        return InventoryItem.TOWER;
    }

    public void consumed()
    {
        // Not consumed
    }

}
