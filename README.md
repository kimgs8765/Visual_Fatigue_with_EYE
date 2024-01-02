# Prediction of Visual fatigue in Virtual Reality, Based on Eye-tracking

<aside>
💡 **Eye-tracking · Video data 기반의 실시간 시각 피로 예측 타당성 검증 및 시각 피로 예측 Model 개발**

</aside>

> **Employed Stacks** 
 `Unity3D` `Pytorch` `OpenCV` `Pandas` `Numpy` `Pingouin` `Statsmodels` `Seaborn` `Matplotlibs`
> 

# Main Ideas


![DL.png](Prediction%20of%20Visual%20fatigue%20in%20Virtual%20Reality,%20B%20ce6b06c53eb44691ae6e1fc32b836a51/DL.png)

1. **실시간 시각 피로 데이터 수집 및 Eye-tracking data로부터 안구 운동 Pattern 수집 및 분석** 
2. **Computer Vision 알고리즘을 기반의 영상 데이터 추출**
3. **Correlation Analysis를 통한 시각 피로와 안구 운동 및 영상 데이터의 관계 분석**
4. **Regression Analysis를 통한 시각 피로 예측 타당성 검정** 
5. **Deep Learning Architecture 활용한 시각 피로 예측 모델 개발** 

# Data Processing

1. **Motion Information** 
    
    → **Lukas-Kanade 알고리즘을 활용하여 영상 내 Motion Vector의 크기 추출**
    
2. **Brightness Information** 
    
    → **모든 Frame의 평균 V-value 추출**
    
3. **Eye-tracking data 전처리를 통한 안구 운동 pattern**

![EYE.png](Prediction%20of%20Visual%20fatigue%20in%20Virtual%20Reality,%20B%20ce6b06c53eb44691ae6e1fc32b836a51/EYE.png)

![I-DT.png](Prediction%20of%20Visual%20fatigue%20in%20Virtual%20Reality,%20B%20ce6b06c53eb44691ae6e1fc32b836a51/I-DT.png)

# Prediction Model Architecture


![Model.png](Prediction%20of%20Visual%20fatigue%20in%20Virtual%20Reality,%20B%20ce6b06c53eb44691ae6e1fc32b836a51/Model.png)

- **Pretrained ResNet18 을 활용한 Video feature extraction**
- **3D-CNN을 활용하여 1-seconds(30FPS) 동안의 Feature를 Extraction**
- **Eye-tracking data와 Video Feature를 결합하여 LSTM Model을 통해 시각피로 예측**

# Results

1. **안구 운동 중 시각 피로와 가장 관계가 깊은 특성이 Blinking number 임을 증명** 
2. **Regression 분석을 통해 Eye-tracking 데이터 기반의 시각 피로 예측의 타당성 증명 (R-squared value : 0.23)**
3. **CNN-LSTM 구조의 시각 피로도 예측 모델 개발 ( In progress )**
4. **Attention Mechanism을 활용한 시각피로 예측 모델 개발 ( In progress )**

Image Reference : https://opencv.org
