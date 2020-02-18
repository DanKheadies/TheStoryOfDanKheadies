// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/17/2020
// Last:  02/18/2020

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_BuildDescriptionBar : MonoBehaviour
{
    public GameObject returnButton;
    public TD_SBF_NodeUI towerUpgradeBar;
    public TD_SBF_Shop shop;
    public Text damageValue;
    public Text healthValue;
    public Text levelValue;
    public Text nameValue;
    public Text rangeValue;
    public Text speedValue;
    public Text speedLabel;

    public void CheckReturnButton()
    {
        if (!towerUpgradeBar.selectionEffect)
        {
            returnButton.GetComponent<Image>().enabled = false;
            returnButton.GetComponent<Button>().enabled = false;
            returnButton.GetComponentInChildren<Text>().enabled = false;
        }
        else
        {
            returnButton.GetComponent<Image>().enabled = true;
            returnButton.GetComponent<Button>().enabled = true;
            returnButton.GetComponentInChildren<Text>().enabled = true;
        }
    }

    public void UpdateValues(string _name)
    {
        if (_name == "")
            _name = towerUpgradeBar.GetComponent<TD_SBF_NodeUI>().target.GetComponent<TD_SBF_Node>().turret.name;

        string name = _name;
        name = name.Replace("TD_SBF_Tower_", "");

        if (towerUpgradeBar.selectionEffect)
            name = name.Remove(name.Length - 7);

        if (name == "Standard_L1")
        {
            damageValue.text = shop.basicTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.basicTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.basicTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.basicTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Standard_L2")
        {
            damageValue.text = shop.basicTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.basicTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.basicTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.basicTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Standard_L3")
        {
            damageValue.text = shop.basicTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.basicTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.basicTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.basicTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Skull_L1")
        {
            damageValue.text = shop.skullTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.skullTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.skullTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.skullTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Skull_L2")
        {
            damageValue.text = shop.skullTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.skullTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.skullTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.skullTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Skull_L3")
        {
            damageValue.text = shop.skullTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.skullTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.skullTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.skullTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Fire_L1")
        {
            damageValue.text = shop.fireTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.fireTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.fireTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.fireTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Fire_L2")
        {
            damageValue.text = shop.fireTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.fireTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.fireTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.fireTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Fire_L3")
        {
            damageValue.text = shop.fireTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.fireTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.fireTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.fireTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Orb_L1")
        {
            damageValue.text = shop.orbTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().damageOverTime.ToString();
            healthValue.text = shop.orbTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.orbTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = ((1 - shop.orbTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().slowAmount) * 100).ToString() + "%";
            speedLabel.text = "Slow";
        }
        else if (name == "Orb_L2")
        {
            damageValue.text = shop.orbTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().damageOverTime.ToString();
            healthValue.text = shop.orbTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.orbTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = ((1 - shop.orbTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().slowAmount) * 100).ToString() + "%";
            speedLabel.text = "Slow";
        }
        else if (name == "Orb_L3")
        {
            damageValue.text = shop.orbTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().damageOverTime.ToString();
            healthValue.text = shop.orbTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.orbTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = ((1 - shop.orbTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().slowAmount) * 100).ToString() + "%";
            speedLabel.text = "Slow";
        }
        else if (name == "Boom_L1")
        {
            damageValue.text = shop.boomTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.boomTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.boomTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.boomTower.lvl1_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Boom_L2")
        {
            damageValue.text = shop.boomTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.boomTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.boomTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.boomTower.lvl2_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else if (name == "Boom_L3")
        {
            damageValue.text = shop.boomTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().bulletPrefab.GetComponent<TD_SBF_Bullet>().damage.ToString();
            healthValue.text = shop.boomTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().startHealth.ToString();
            levelValue.text = name.Substring(name.Length - 1).ToString();
            nameValue.text = name.Remove(name.Length - 3);
            rangeValue.text = shop.boomTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().range.ToString();
            speedValue.text = shop.boomTower.lvl3_prefab.GetComponent<TD_SBF_Turret>().fireRate.ToString();
            speedLabel.text = "Speed";
        }
        else
        {
            damageValue.text = "n/a";
            healthValue.text = "n/a";
            levelValue.text = "n/a";
            nameValue.text = "n/a";
            rangeValue.text = "n/a";
            speedValue.text = "n/a";
            speedLabel.text = "Speed";
        }
    }
}
