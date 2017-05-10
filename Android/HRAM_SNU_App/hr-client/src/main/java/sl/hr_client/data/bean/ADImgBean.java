package sl.hr_client.data.bean;


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
