

//ロード時、サポートされていない場合は非表示にする
if (navigator.share) {
    console.log("navigator.share is supported!");
} else {
    console.log("navigator.share is not supported!");

    //画面のネイティブ共有ボタンを非表示にする(shareLink属性がついているタグを全て非表示にする)
    var shareLinks = Array.prototype.slice.call(document.getElementsByTagName("a"));
    shareLinks.filter(item => item.hasAttribute("shareLink")).forEach(item => item.style.display = "none");
}