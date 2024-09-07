using R3;
using System;
using UnityEngine;

public class R3Samples : MonoBehaviour
{
    


    private void Start()
    {
        Example10();
    }

    // Example1
    private ReactiveProperty<int> health;

    public void Example1()
    {
        health = new ReactiveProperty<int>(200);
        health.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });

        health.Value = 100;
        health.Value = 90;
        health.Value -= 50;
        health.Value += 120;

    }

    // Example2
    public ReadOnlyReactiveProperty<int> Health2 => health2;
    private readonly ReactiveProperty<int> health2 = new ReactiveProperty<int>();

    public void Example2()
    {
        Health2.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });

        health2.Value = 110;
        health2.Value = 91;
        health2.Value -= 51;
        health2.Value += 121;
    }

    // Example3
    public Observable<int> Health3 => health3;
    private readonly ReactiveProperty<int> health3 = new ReactiveProperty<int>();

    public void Example3()
    {
        Health3.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });

        health3.Value = 12;
        health3.Value = 92;
        health3.Value -= 52;
        health3.Value += 122;
    }

    // Example4
    public Observable<int> HealthChanged => healthChanged;
    private readonly Subject<int> healthChanged = new Subject<int>();

    public void Example4()
    {
        healthChanged.OnNext(33);

        HealthChanged.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });
        
        healthChanged.OnNext(15); 
        healthChanged.OnNext(18);
    }

    // Example5
    public Observable<int> Health5 => health5;
    private readonly ReactiveProperty<int> health5 = new ReactiveProperty<int>();
    private IDisposable disposable5;

    public void Example5()
    {
        health5.Value = 1000;

        disposable5 = Health5.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });

        health5.Value = 1500;

        disposable5.Dispose();

        health5.Value = 60000;
    }

    // Example6
    public Observable<int> Health6 => health6;
    public Observable<int> Armor6 => armor6;

    private readonly ReactiveProperty<int> health6 = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> armor6 = new ReactiveProperty<int>();
    private CompositeDisposable compositDisposable6 = new CompositeDisposable();

    public void Example6()
    {
        health6.Value = 1;
        armor6.Value = 1;

        var subscriptionHealth = Health6.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });
        var subscriptionArmor = Armor6.Subscribe(newValue => { Debug.Log($"Armor: {newValue}"); });
        compositDisposable6.Add(subscriptionHealth);
        compositDisposable6.Add(subscriptionArmor);

        health6.Value = 100;
        armor6.Value = 100;

        compositDisposable6.Dispose();

        health6.Value = 18;
        armor6.Value = 18;
    }


    // Example7
    public Observable<int> Health7 => health7;
    public Observable<int> Armor7 => armor7;

    private readonly ReactiveProperty<int> health7 = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> armor7 = new ReactiveProperty<int>();
    

    public void Example7()
    {
        Health7.Merge(Armor7).Subscribe(_ =>
        {
            Debug.Log($"Event. Health: {health7.CurrentValue}, Armor: {armor7.CurrentValue}");
        });

        health7.Value = 1;
        armor7.Value = 1;

        health7.Value = 8;
        armor7.Value = 10;


        health7.Value = 12;
        health7.Value = 15;
        armor7.Value = 20;
        armor7.Value = 25;
    }

    // Example8
    public Observable<int> Health8 => health8;

    private readonly ReactiveProperty<int> health8 = new ReactiveProperty<int>();

    public void Example8()
    {
        Health8.Where(health => health > 50).Subscribe(_ =>
        {
            Debug.Log($"Event. Health: {health8.CurrentValue}");
        });

        health8.Value = 1;
        health8.Value = 60;
        health8.Value = 32;
        health8.Value = 98;
    }


    //Example10
    public event Action<int> ValueChanged10;
    
    public void Example10()
    {
        Observable.FromEvent<int>(action => ValueChanged10 += action, action => ValueChanged10 -= action)
            .Subscribe(value =>
            {
                Debug.Log("Value: " + value);
            });

        ValueChanged10?.Invoke(10);
        ValueChanged10?.Invoke(20);
    }
}
