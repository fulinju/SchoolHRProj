package sl.hr_client.data.bean;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;

import java.util.List;
import org.greenrobot.greendao.annotation.Generated;
import org.greenrobot.greendao.annotation.JoinEntity;
import org.greenrobot.greendao.annotation.Keep;
import org.greenrobot.greendao.annotation.ToMany;
import org.greenrobot.greendao.annotation.ToOne;
import org.greenrobot.greendao.annotation.Transient;

/**
 * Created by Administrator on 2017/4/6.
 */
@Entity
public class NewsBean {
    @Id
    private String publishID;
    private String uUserName;
    private String pmPublishTime;
    private String pmTypeValue;
    private String pmViews;
    private String pmTitle;
    private String pmText;

    private String pmADImgListJson; //pmADImgList的JSON

    @Transient
    public List<ADImgBean> pmADImgList;

    @Generated(hash = 233967757)
    public NewsBean(String publishID, String uUserName, String pmPublishTime,
            String pmTypeValue, String pmViews, String pmTitle, String pmText,
            String pmADImgListJson) {
        this.publishID = publishID;
        this.uUserName = uUserName;
        this.pmPublishTime = pmPublishTime;
        this.pmTypeValue = pmTypeValue;
        this.pmViews = pmViews;
        this.pmTitle = pmTitle;
        this.pmText = pmText;
        this.pmADImgListJson = pmADImgListJson;
    }

    @Generated(hash = 1662878226)
    public NewsBean() {
    }

    public List<ADImgBean> getPmADImgList() {
        return pmADImgList;
    }

    public void setPmADImgList(List<ADImgBean> pmADImgList) {
        this.pmADImgList = pmADImgList;
    }

    public String getPmADImgListJson() {
        return this.pmADImgListJson;
    }

    public void setPmADImgListJson(String pmADImgListJson) {
        this.pmADImgListJson = pmADImgListJson;
    }

    public String getPmText() {
        return this.pmText;
    }

    public void setPmText(String pmText) {
        this.pmText = pmText;
    }

    public String getPmTitle() {
        return this.pmTitle;
    }

    public void setPmTitle(String pmTitle) {
        this.pmTitle = pmTitle;
    }

    public String getPmViews() {
        return this.pmViews;
    }

    public void setPmViews(String pmViews) {
        this.pmViews = pmViews;
    }

    public String getPmTypeValue() {
        return this.pmTypeValue;
    }

    public void setPmTypeValue(String pmTypeValue) {
        this.pmTypeValue = pmTypeValue;
    }

    public String getPmPublishTime() {
        return this.pmPublishTime;
    }

    public void setPmPublishTime(String pmPublishTime) {
        this.pmPublishTime = pmPublishTime;
    }

    public String getUUserName() {
        return this.uUserName;
    }

    public void setUUserName(String uUserName) {
        this.uUserName = uUserName;
    }

    public String getPublishID() {
        return this.publishID;
    }

    public void setPublishID(String publishID) {
        this.publishID = publishID;
    }



}
