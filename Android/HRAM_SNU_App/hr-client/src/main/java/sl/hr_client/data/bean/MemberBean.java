package sl.hr_client.data.bean;

import org.greenrobot.greendao.annotation.Entity;
import org.greenrobot.greendao.annotation.Id;
import org.greenrobot.greendao.annotation.Generated;

/**
 * Created by Administrator on 2017/4/24.
 */
@Entity
public class MemberBean {
    @Id
    private String memberID;
    private String uLoginName;
    private String uUserName;
    private String mReviewResultID;
    private String mReviewResultValue;
    private String mApplyTime;
    private String mName;
    private String mTypeID;
    private String mTypeValue;
    private String mOrganizationCode;
    private String mAddress;
    private String mCorporateName;
    private String mIDCardNo;
    private String mContacts;
    private String mContactsPhone;
    private String mSummary;
    private String mImgURL;
    private String mURL;
    public String getMURL() {
        return this.mURL;
    }
    public void setMURL(String mURL) {
        this.mURL = mURL;
    }
    public String getMImgURL() {
        return this.mImgURL;
    }
    public void setMImgURL(String mImgURL) {
        this.mImgURL = mImgURL;
    }
    public String getMSummary() {
        return this.mSummary;
    }
    public void setMSummary(String mSummary) {
        this.mSummary = mSummary;
    }
    public String getMContactsPhone() {
        return this.mContactsPhone;
    }
    public void setMContactsPhone(String mContactsPhone) {
        this.mContactsPhone = mContactsPhone;
    }
    public String getMContacts() {
        return this.mContacts;
    }
    public void setMContacts(String mContacts) {
        this.mContacts = mContacts;
    }
    public String getMIDCardNo() {
        return this.mIDCardNo;
    }
    public void setMIDCardNo(String mIDCardNo) {
        this.mIDCardNo = mIDCardNo;
    }
    public String getMCorporateName() {
        return this.mCorporateName;
    }
    public void setMCorporateName(String mCorporateName) {
        this.mCorporateName = mCorporateName;
    }
    public String getMAddress() {
        return this.mAddress;
    }
    public void setMAddress(String mAddress) {
        this.mAddress = mAddress;
    }
    public String getMOrganizationCode() {
        return this.mOrganizationCode;
    }
    public void setMOrganizationCode(String mOrganizationCode) {
        this.mOrganizationCode = mOrganizationCode;
    }
    public String getMTypeValue() {
        return this.mTypeValue;
    }
    public void setMTypeValue(String mTypeValue) {
        this.mTypeValue = mTypeValue;
    }
    public String getMTypeID() {
        return this.mTypeID;
    }
    public void setMTypeID(String mTypeID) {
        this.mTypeID = mTypeID;
    }
    public String getMName() {
        return this.mName;
    }
    public void setMName(String mName) {
        this.mName = mName;
    }
    public String getMApplyTime() {
        return this.mApplyTime;
    }
    public void setMApplyTime(String mApplyTime) {
        this.mApplyTime = mApplyTime;
    }
    public String getMReviewResultValue() {
        return this.mReviewResultValue;
    }
    public void setMReviewResultValue(String mReviewResultValue) {
        this.mReviewResultValue = mReviewResultValue;
    }
    public String getMReviewResultID() {
        return this.mReviewResultID;
    }
    public void setMReviewResultID(String mReviewResultID) {
        this.mReviewResultID = mReviewResultID;
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
    public String getMemberID() {
        return this.memberID;
    }
    public void setMemberID(String memberID) {
        this.memberID = memberID;
    }
    @Generated(hash = 1970424065)
    public MemberBean(String memberID, String uLoginName, String uUserName,
            String mReviewResultID, String mReviewResultValue, String mApplyTime,
            String mName, String mTypeID, String mTypeValue,
            String mOrganizationCode, String mAddress, String mCorporateName,
            String mIDCardNo, String mContacts, String mContactsPhone,
            String mSummary, String mImgURL, String mURL) {
        this.memberID = memberID;
        this.uLoginName = uLoginName;
        this.uUserName = uUserName;
        this.mReviewResultID = mReviewResultID;
        this.mReviewResultValue = mReviewResultValue;
        this.mApplyTime = mApplyTime;
        this.mName = mName;
        this.mTypeID = mTypeID;
        this.mTypeValue = mTypeValue;
        this.mOrganizationCode = mOrganizationCode;
        this.mAddress = mAddress;
        this.mCorporateName = mCorporateName;
        this.mIDCardNo = mIDCardNo;
        this.mContacts = mContacts;
        this.mContactsPhone = mContactsPhone;
        this.mSummary = mSummary;
        this.mImgURL = mImgURL;
        this.mURL = mURL;
    }
    @Generated(hash = 1592035565)
    public MemberBean() {
    }
    
}
