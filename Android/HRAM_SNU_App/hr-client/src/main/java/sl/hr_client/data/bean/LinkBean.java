package sl.hr_client.data.bean;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;
import org.greenrobot.greendao.annotation.Generated;

/**
 * Created by Administrator on 2017/4/24.
 */

@Entity
public class LinkBean {
    @Id
    private String friendlyLinkID;
    private String uLoginName;
    private String uUserName;
    private String flTypeValue;
    private String flName;
    private String flURL;
    private String flImgURL;
    private String flAddTime;
    private boolean isDeleted;
    public boolean getIsDeleted() {
        return this.isDeleted;
    }
    public void setIsDeleted(boolean isDeleted) {
        this.isDeleted = isDeleted;
    }
    public String getFlAddTime() {
        return this.flAddTime;
    }
    public void setFlAddTime(String flAddTime) {
        this.flAddTime = flAddTime;
    }
    public String getFlImgURL() {
        return this.flImgURL;
    }
    public void setFlImgURL(String flImgURL) {
        this.flImgURL = flImgURL;
    }
    public String getFlURL() {
        return this.flURL;
    }
    public void setFlURL(String flURL) {
        this.flURL = flURL;
    }
    public String getFlName() {
        return this.flName;
    }
    public void setFlName(String flName) {
        this.flName = flName;
    }
    public String getFlTypeValue() {
        return this.flTypeValue;
    }
    public void setFlTypeValue(String flTypeValue) {
        this.flTypeValue = flTypeValue;
    }
    public String getUUserName() {
        return this.uUserName;
    }
    public void setUUserName(String uUserName) {
        this.uUserName = uUserName;
    }
    public String getULoginName() {
        return this.uLoginName;
    }
    public void setULoginName(String uLoginName) {
        this.uLoginName = uLoginName;
    }
    public String getFriendlyLinkID() {
        return this.friendlyLinkID;
    }
    public void setFriendlyLinkID(String friendlyLinkID) {
        this.friendlyLinkID = friendlyLinkID;
    }
    @Generated(hash = 708113196)
    public LinkBean(String friendlyLinkID, String uLoginName, String uUserName,
            String flTypeValue, String flName, String flURL, String flImgURL,
            String flAddTime, boolean isDeleted) {
        this.friendlyLinkID = friendlyLinkID;
        this.uLoginName = uLoginName;
        this.uUserName = uUserName;
        this.flTypeValue = flTypeValue;
        this.flName = flName;
        this.flURL = flURL;
        this.flImgURL = flImgURL;
        this.flAddTime = flAddTime;
        this.isDeleted = isDeleted;
    }
    @Generated(hash = 1730739555)
    public LinkBean() {
    }

}
