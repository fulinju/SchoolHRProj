package sl.hr_client.data.bean;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;
import org.greenrobot.greendao.annotation.Generated;

/**
 * Created by Administrator on 2017/4/25.
 */

@Entity
public class UserBean {
    @Id
    private String uID;
    private String uLoginStr;
    private String uLoginTypeID;
    private String uUserName;
    private String uPhone;
    private String uMaiBox;
    private String uClientKey;
    private String uToken;
    private String uTokenActiveTime;
    private String uTokenExpiredTime;
    public String getUTokenExpiredTime() {
        return this.uTokenExpiredTime;
    }
    public void setUTokenExpiredTime(String uTokenExpiredTime) {
        this.uTokenExpiredTime = uTokenExpiredTime;
    }
    public String getUTokenActiveTime() {
        return this.uTokenActiveTime;
    }
    public void setUTokenActiveTime(String uTokenActiveTime) {
        this.uTokenActiveTime = uTokenActiveTime;
    }
    public String getUToken() {
        return this.uToken;
    }
    public void setUToken(String uToken) {
        this.uToken = uToken;
    }
    public String getUClientKey() {
        return this.uClientKey;
    }
    public void setUClientKey(String uClientKey) {
        this.uClientKey = uClientKey;
    }
    public String getUMaiBox() {
        return this.uMaiBox;
    }
    public void setUMaiBox(String uMaiBox) {
        this.uMaiBox = uMaiBox;
    }
    public String getUPhone() {
        return this.uPhone;
    }
    public void setUPhone(String uPhone) {
        this.uPhone = uPhone;
    }
    public String getUUserName() {
        return this.uUserName;
    }
    public void setUUserName(String uUserName) {
        this.uUserName = uUserName;
    }
    public String getULoginTypeID() {
        return this.uLoginTypeID;
    }
    public void setULoginTypeID(String uLoginTypeID) {
        this.uLoginTypeID = uLoginTypeID;
    }
    public String getULoginStr() {
        return this.uLoginStr;
    }
    public void setULoginStr(String uLoginStr) {
        this.uLoginStr = uLoginStr;
    }
    public String getUID() {
        return this.uID;
    }
    public void setUID(String uID) {
        this.uID = uID;
    }
    @Generated(hash = 1051158266)
    public UserBean(String uID, String uLoginStr, String uLoginTypeID,
            String uUserName, String uPhone, String uMaiBox, String uClientKey,
            String uToken, String uTokenActiveTime, String uTokenExpiredTime) {
        this.uID = uID;
        this.uLoginStr = uLoginStr;
        this.uLoginTypeID = uLoginTypeID;
        this.uUserName = uUserName;
        this.uPhone = uPhone;
        this.uMaiBox = uMaiBox;
        this.uClientKey = uClientKey;
        this.uToken = uToken;
        this.uTokenActiveTime = uTokenActiveTime;
        this.uTokenExpiredTime = uTokenExpiredTime;
    }
    @Generated(hash = 1203313951)
    public UserBean() {
    }
}
