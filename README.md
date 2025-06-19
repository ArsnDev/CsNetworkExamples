# C# Network Examples

.NET 8.0 환경에서 C#으로 작성된 다양한 비동기 소켓 통신 예제 코드입니다.  
APM, EAP, TAP 세 가지 비동기 프로그래밍 패턴을 각각 클라이언트/서버로 구현하여 비교 학습할 수 있습니다.

---

## 📁 프로젝트 구조

```
CsNetworkExamples/
  ├── APM/   # Asynchronous Programming Model
  │   ├── APM_Client/
  │   └── APM_Server/
  ├── EAP/   # Event-based Asynchronous Pattern
  │   ├── EAP_Client/
  │   └── EAP_Server/
  └── TAP/   # Task-based Asynchronous Pattern
      ├── TAP_Client/
      └── TAP_Server/
```

- 각 폴더는 해당 비동기 패턴의 클라이언트/서버 예제 프로젝트를 포함합니다.

---

## 📝 예제 설명

- **APM**: IAsyncResult 기반의 전통적인 비동기 패턴
- **EAP**: 이벤트 기반 비동기 패턴 (이벤트 핸들러 사용)
- **TAP**: async/await와 Task를 활용한 현대적인 비동기 패턴

각 예제는 콘솔 애플리케이션으로, 클라이언트가 메시지를 입력하면 서버로 전송하고, 서버는 받은 메시지를 콘솔에 출력합니다.

---

## ⚙️ 실행 환경

- .NET 8.0 SDK 이상

---

## 🚀 실행 방법

1. **서버 실행**
   ```bash
   # 예시: TAP 방식 서버 실행
   cd TAP/TAP_Server
   dotnet run
   ```

2. **클라이언트 실행**
   ```bash
   # 예시: TAP 방식 클라이언트 실행
   cd TAP/TAP_Client
   dotnet run
   ```

3. 클라이언트 콘솔에 메시지를 입력하면 서버 콘솔에서 해당 메시지를 확인할 수 있습니다.

---

## 💡 참고

- .NET 8.0이 설치되어 있어야 합니다.
- 본 프로젝트는 각 비동기 방식의 기본적인 동작을 보여주기 위한 예제 코드입니다. 따라서 안정적인 종료(Graceful Shutdown)와 같은 예외 처리는 구현되어 있지 않습니다.