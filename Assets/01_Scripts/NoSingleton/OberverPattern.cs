using UnityEngine;

using System.Collections.Generic;
using System;

// 상태 변경 알림을 받기 위한 인터페이스 정의
public interface IObserver
{
    void PlayerDead();

    void EnemyDead(Transform transform);

    void EnemyRelease(GameObject gameObject);

    void ReleaseBox(GameObject box);

    void TurnOffBackground();

}

// 옵저버 등록, 해제, 알림을 위한 인터페이스 정의
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}
