# 🌌 AR Gravity Physics Simulation (Unity + ARCore)

🎓 *An Augmented Reality educational app for students to visualize how gravity works across different planets using real-time phone movement.*

### 📲 [🔗 Download APK](https://drive.google.com/uc?export=download&id=1mbPREk_IDP81C9fUK3LLvVWVasRkHLNc)

> ✅ Tested on ARCore-supported Android devices (Android 8.0+)

## 🔧 Setup Instructions

### ✅ Requirements
- Unity 6 or later
- AR Foundation
- ARCore XR Plugin (via Package Manager)
- Android phone with ARCore support

### 🔨 Steps
1. **Install AR Foundation & ARCore XR Plugin** via Unity Package Manager.
2. **Scene Setup**:
   - Add `AR Session` and `AR Session Origin`
   - Add `ARPlaneManager` and assign a plane prefab (with MeshRenderer & MeshCollider)
   - Add `ARRaycastManager`
   - Create a GameObject named `SpawnPoint` just above the detected plane
3. **Attach Script**:
   - Create an empty `GameManager` and add `ARPhysicsController.cs`
   - Link all references in the Inspector (UI, prefabs, spawn point)
4. **Player Settings**:
   - Switch to Android
   - Enable **ARCore Support**
   - Set minimum API level to **Android 7.0 (Nougat)** or higher
5. **Build & Run** on your AR-supported Android device

---

## 🎯 Features

- 📱 AR Plane Detection using ARCore
- 🧲 Gravity direction updates based on **phone tilt**
- 🔻 Drop Shape: Ball / Cube / Capsule
- 🌍 Planet Selector: Earth, Moon, Mars (changes gravity)
- 🎛 Adjustable gravity slider
- 🧪 Real-time velocity display
- 🔁 One-tap Reset button

---

## 🎓 Educational Value

- Understand planetary gravity (Earth, Moon, Mars)
- See object motion & velocity in real-time
- Feel the impact of gravitational direction with device tilt
- Learn physics concepts through direct AR interaction

---

## 🧪 How to Use

1. Install APK on your Android phone
2. Move phone to detect a plane (floor/table)
3. Choose a shape and planet
4. Tap **Drop Shape**
5. Tilt phone to see the object fall and react
6. Tap **Reset** to start again

---

## 🛠️ Customization Ideas

- Add material-based friction and bounce
- Add shadows or decals on surface
- Add fixed vs tilt gravity toggle

---

## 👤 Author

**Sayan Jana**  
Unity Game Developer | AR/VR | Physics  
🔗 [LinkedIn](https://www.linkedin.com/in/sayanforsayan)  
🌐 [Portfolio](https://www.sayanforsayan.com)  
💻 [GitHub](https://github.com/sayanforsayan)

---

## 📘 License

This project is for educational use. You are free to modify and share with credit.
