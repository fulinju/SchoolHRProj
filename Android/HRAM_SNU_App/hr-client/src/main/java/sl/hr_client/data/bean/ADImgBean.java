package sl.hr_client.data.bean;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;
import org.greenrobot.greendao.annotation.Generated;

import java.io.Serializable;

/**
 * Created by Administrator on 2017/4/6.
 */
public class ADImgBean implements Serializable{
    private String pmADImgListURL;
    private String pmADImgListNum;
    public String getPmADImgListNum() {
        return this.pmADImgListNum;
    }
    public void setPmADImgListNum(String pmADImgListNum) {
        this.pmADImgListNum = pmADImgListNum;
    }
    public String getPmADImgListURL() {
        return this.pmADImgListURL;
    }
    public void setPmADImgListURL(String pmADImgListURL) {
        this.pmADImgListURL = pmADImgListURL;
    }
}
