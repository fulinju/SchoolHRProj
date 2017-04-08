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
    private Long id;
    private String uUserName;
    private String pmPublishTime;
    private String pmTypeValue;
    private String pmViews;
    private String pmTitle;
    private String pmText;

    private String pmADImgListJson; //pmADImgList的JSON

    @Transient
    public List<ADImgBean> pmADImgList;

    public List<ADImgBean> getPmADImgList() {
        return pmADImgList;
    }

    public void setPmADImgList(List<ADImgBean> pmADImgList) {
        this.pmADImgList = pmADImgList;
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
    public Long getId() {
        return this.id;
    }
    public void setId(Long id) {
        this.id = id;
    }
    public String getPmADImgListJson() {
        return this.pmADImgListJson;
    }
    public void setPmADImgListJson(String pmADImgListJson) {
        this.pmADImgListJson = pmADImgListJson;
    }

    public String getPmTypeValue() {
        return this.pmTypeValue;
    }

    public void setPmTypeValue(String pmTypeValue) {
        this.pmTypeValue = pmTypeValue;
    }
    @Generated(hash = 13241424)
    public NewsBean(Long id, String uUserName, String pmPublishTime,
            String pmTypeValue, String pmViews, String pmTitle, String pmText,
            String pmADImgListJson) {
        this.id = id;
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


}
