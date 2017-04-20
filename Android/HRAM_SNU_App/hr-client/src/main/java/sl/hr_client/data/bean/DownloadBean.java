package sl.hr_client.data.bean;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;
import org.greenrobot.greendao.annotation.Generated;

/**
 * Created by Administrator on 2017/4/12.
 */
@Entity
public class DownloadBean {
    @Id
    private Long id;
    private String downloadID;
    private String uLoginName;
    private String uUserName;
    private String dmTitle;
    private String dmTypeID;
    private String dmTypeValue;
    private String dmFileURL;
    private int dmDownloadNum;
    private String dmUploadTime;
    public String getDmUploadTime() {
        return this.dmUploadTime;
    }
    public void setDmUploadTime(String dmUploadTime) {
        this.dmUploadTime = dmUploadTime;
    }
    public int getDmDownloadNum() {
        return this.dmDownloadNum;
    }
    public void setDmDownloadNum(int dmDownloadNum) {
        this.dmDownloadNum = dmDownloadNum;
    }
    public String getDmFileURL() {
        return this.dmFileURL;
    }
    public void setDmFileURL(String dmFileURL) {
        this.dmFileURL = dmFileURL;
    }
    public String getDmTypeValue() {
        return this.dmTypeValue;
    }
    public void setDmTypeValue(String dmTypeValue) {
        this.dmTypeValue = dmTypeValue;
    }
    public String getDmTypeID() {
        return this.dmTypeID;
    }
    public void setDmTypeID(String dmTypeID) {
        this.dmTypeID = dmTypeID;
    }
    public String getDmTitle() {
        return this.dmTitle;
    }
    public void setDmTitle(String dmTitle) {
        this.dmTitle = dmTitle;
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
    public String getDownloadID() {
        return this.downloadID;
    }
    public void setDownloadID(String downloadID) {
        this.downloadID = downloadID;
    }
    public Long getId() {
        return this.id;
    }
    public void setId(Long id) {
        this.id = id;
    }
    @Generated(hash = 1771300858)
    public DownloadBean(Long id, String downloadID, String uLoginName,
            String uUserName, String dmTitle, String dmTypeID, String dmTypeValue,
            String dmFileURL, int dmDownloadNum, String dmUploadTime) {
        this.id = id;
        this.downloadID = downloadID;
        this.uLoginName = uLoginName;
        this.uUserName = uUserName;
        this.dmTitle = dmTitle;
        this.dmTypeID = dmTypeID;
        this.dmTypeValue = dmTypeValue;
        this.dmFileURL = dmFileURL;
        this.dmDownloadNum = dmDownloadNum;
        this.dmUploadTime = dmUploadTime;
    }
    @Generated(hash = 2040406903)
    public DownloadBean() {
    }

}
