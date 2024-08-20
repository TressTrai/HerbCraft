mergeInto(LibraryManager.library, {

  PlayerData: function () {
    console.log("Функция работает")
    console.log(player.getName());
    console.log(player.getPhoto("medium"));
  },

  RateGame: function () {
    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) {
        ysdk.feedback.requestReview()
        .then(({ feedbackSent }) => {
          console.log(feedbackSent);
        })
      } else {
        console.log(reason)
      }
    })
  },

  SetToLeaderBord: function (value) {
    ysdk.getLeaderboards()
      .then(lb => {
        // Без extraData.
        lb.setLeaderboardScore('Money', value);
    });
  },

  RewardedApp: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
          onOpen: () => {
            console.log('Video ad open.');
          },
          onRewarded: () => {
            myGameInstance.SendMessage("HealthBar/Canvas/HelthBarWithScript", "RespawnWithAward");
          },
          onClose: () => {
            console.log('Video ad closed.');
          },
          onError: () => {
            myGameInstance.SendMessage("HealthBar/Canvas/HelthBarWithScript", "Respawn");
      }
    }
   })
  },

  GetDevice: function () {
    console.log(ysdk.deviceInfo.type);
    return ysdk.deviceInfo.type;
  },

});