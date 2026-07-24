CardPath=/home/homeusr/RiderProjects/DeltaWarriors/DeltaWarriors/images/card_portraits
mogrify -path "${CardPath}" -resize 50% "${CardPath}/big/*.png"
mogrify -path "${CardPath}/beta" -resize 50% "${CardPath}/big/beta/*.png"